using System;
using System.Collections.Generic;
using System.Linq;
using WcfJsonRestService.DB;
using WcfJsonRestService.Extensions;
using WcfJsonRestService.Model;
using WcfJsonRestService.WebModel;
using Person = WcfJsonRestService.WebModel.Person;

namespace WcfJsonRestService
{
    public class Service1 : IService1
    {
        //GET: /petitions
        public List<PetitionShort> GetAllPetitions ( )
        {
            List<PetitionShort> petitions = null;

            using ( var db = new PetitionContext () )
            {
                petitions = ( from r in db.Petitions select r ).Select ( petition => petition ).ToList ().ToWeb ();
            }

            return petitions;
        }

        //GET: /petitions/{petitionId}
        public PetitionNormal GetPetition ( string petitionId )
        {
            PetitionNormal petition = null;

            using ( var db = new PetitionContext () )
            {
                petition = db.Petitions.Find ( petitionId.ToInt () ).ToNormalWeb ();
            }

            return petition;
        }

        //GET: /petitions?own=true - TODO owner is hardcoded now, will be fixed after integration with fb
        public List<PetitionShort> GetPetitionForCurrentUser ( )
        {
            List<PetitionShort> petitions = null;

            using ( var db = new PetitionContext () )
            {
                var currentUser = db.People.Find ( 1 );

                petitions = ( from r in db.Petitions select r )
                    .Where ( petition => petition.Creator.PersonId == currentUser.PersonId )
                    .ToList ().ToWeb ();
            }

            return petitions;
        }

        //POST: /petitions - TODO owner is hardcoded now, will be fixed after integration with fb
        public int AddPetition ( RequestBody requestBody )
        {
            int id;
            using ( var db = new PetitionContext () )
            {
                var creator = db.People.Find ( 1 ); //TODO should be user based on secret from api (I think..)
                var petition = new Model.Petition ()
                {
                    Title = requestBody.Title,
                    Tags = requestBody.Tags.ToDbModel (),
                    Text = requestBody.Text,
                    Url = requestBody.ImageUrl,
                    Creator = creator,
                    CreationDate = DateTime.Now,
                    Members = new List<Model.Person> () { creator }
                };
                db.Petitions.Add ( petition );
                db.SaveChanges ();

                id = petition.PetitionId;
            }

            return id;
        }

        //POST: /petitions/{petitionId}/sign  - TODO owner is hardcoded now, will be fixed after integration with fb
        public void SignPetition ( string petitionId )
        {
            using ( var db = new PetitionContext () )
            {
                var signer = db.People.Find ( 1 ); //TODO should be user based on secret from api (I think..)
                var petition = db.Petitions.Find ( petitionId.ToInt () );

                if ( petition != null )
                {
                    petition.Members.Add ( signer );

                    if ( !petition.Members.Any ( p => p.PersonId == signer.PersonId ) )
                    {
                        db.Petitions.Add ( petition );
                        db.SaveChanges ();

                        MyCustomLogging.Log ( "added and saved changes" );
                    }
                    else
                    {
                        MyCustomLogging.Log ( "not added and saved changes - not needed" );
                    }
                }
                else
                {
                    MyCustomLogging.Log ( "petition with given id ({0}) does not exist", petitionId );
                }
            }
        }

        //TODO not implemented at all due to current DB Schema
        ///petitions/{petitionId}
        public void DeletePetition ( string petitionId )
        {
            using ( var db = new PetitionContext () )
            {
                var petition = db.Petitions.Find ( petitionId.ToInt () );

                //TODO check if petition owner is currently logged user

                if ( petition != null )
                {
                    /*petition.Members.Add ( signer );

                    db.Petitions.Add ( petition );
                    db.SaveChanges ();*/
                    Console.WriteLine ( "petition with given id ({0}) is deleted here. But still exists.", petitionId );
                }
                else
                {
                    Console.WriteLine ( "petition with given id ({0}) does not exist", petitionId );
                }
            }
        }


        #region not used normaly, usable for 'tests'

        public Person GetData ( string id )
        {
            // lookup person with the requested id 
            return new Person ()
            {
                Id = Convert.ToInt32 ( id ),
                Name = "Leo Messi"
            };
        }

        public Result AddPerson ( string name )
        {
            try
            {
                using ( var db = new PetitionContext () )
                {
                    var person = new Model.Person { Name = name };
                    db.People.Add ( person );
                    db.SaveChanges ();
                }

                return new Result () { Success = true };
            }
            catch ( Exception e )
            {
                return new Result () { Success = false, Message = e.ToString () };
            }
        }

        public RequestBody GetSampleRequestBody ( )
        {
            return new RequestBody ()
            {
                ImageUrl = "jakis url obrazka",
                Tags = new List<string> () { "tag1", "tag2", "tag3" },
                Text = "sample txt",
                Title = "My mega title"
            };
        }

        #endregion
    }
}
