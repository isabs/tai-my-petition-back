using System;
using System.Collections.Generic;
using System.Linq;
using WcfJsonRestService.DB;
using WcfJsonRestService.Extensions;
using WcfJsonRestService.Facebook;
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
                petitions = ( from r in db.Petitions select r )
                    .Where ( petition => petition.IsValid )
                    .Select ( petition => petition )
                    .ToList ().ToWeb ();
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

        //GET: /petitions?own=true
        public List<PetitionShort> GetPetitionForCurrentUser ( )
        {
            List<PetitionShort> petitions = null;

            var cm = new ConnectionManager ();
            var ht = new HeaderTools ();

            var token = ht.GetAccesKey ();
            if ( cm.ValidateToken ( token ) )
            {
                var id = cm.GetCurrUserId ( token );

                using ( var db = new PetitionContext () )
                {
                    var currentUser = db.People.Find ( id );

                    petitions = ( from r in db.Petitions select r )
                        .Where ( petition => petition.Creator.PersonId == currentUser.PersonId )
                        .ToList ().ToWeb ();
                }
            }

            return petitions;
        }

        //POST: /petitions 
        public int AddPetition ( RequestBody requestBody )
        {
            int end = 0;
            var cm = new ConnectionManager ();
            var ht = new HeaderTools ();

            var token = ht.GetAccesKey ();
            if ( cm.ValidateToken ( token ) )
            {
                var id = cm.GetCurrUserId ( token );

                using ( var db = new PetitionContext () )
                {
                    var creator = db.People.Find ( id );
                    var petition = new Model.Petition ()
                    {
                        Title = requestBody.Title,
                        Addressee = requestBody.Addressee,
                        Tags = requestBody.Tags.ToDbModel (),
                        Text = requestBody.Text,
                        Url = requestBody.ImageUrl,
                        Creator = creator,
                        CreationDate = DateTime.Now,
                        Members = new List<Model.Person> () { creator }
                    };
                    db.Petitions.Add ( petition );
                    db.SaveChanges ();

                    end = petition.PetitionId;
                }
            }
            return end;
        }

        //POST: /petitions/{petitionId}/sign
        public void SignPetition ( string petitionId )
        {
            var cm = new ConnectionManager ();
            var ht = new HeaderTools ();

            var token = ht.GetAccesKey ();
            if ( !cm.ValidateToken ( token ) )
            {
                MyCustomLogging.Log ( "err" );
                return;
            }


            using ( var db = new PetitionContext () )
            {
                var signer = db.People.Find ( token );
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

        ///petitions/{petitionId}
        public void DeletePetition ( string petitionId )
        {
            using ( var db = new PetitionContext () )
            {
                var petition = db.Petitions.Find ( petitionId.ToInt () );

                if ( petition != null )
                {
                    petition.IsValid = false;
                    db.SaveChanges ();
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

        public void CreateSamples ( )
        {
            using ( var db = new PetitionContext () )
            {
                db.Petitions.Clear ();
                db.People.Clear ();
                db.Tags.Clear ();
            }

            DataGenerator dg = new DataGenerator ();
            dg.GenerateAll ();
        }

        #endregion
    }
}
