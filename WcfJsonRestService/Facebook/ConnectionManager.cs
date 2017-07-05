using System;
using System.Collections.Generic;
using System.Linq;
using Facebook;
using WcfJsonRestService.DB;

namespace WcfJsonRestService.Facebook
{
    public class ConnectionManager
    {
        private FacebookClient Fb { get; set; }

        public ConnectionManager ( )
        {
            Fb = new FacebookClient
            {
                AppId = "125654228008369",
                AppSecret = "19c84dba4226a7e6cd284c6886ec6879"
            };
        }

        public bool ValidateToken ( string accessToken ) //get accesstoken from frontend
        {
            dynamic result = Fb.Get ( "/debug_token", new { input_token = accessToken } );

            return (bool) result.is_valid;
        }

        public int GetCurrUserId ( string accessToken )
        {
            dynamic fbid = Fb.Get ( "/me", new { access_token = accessToken, fields = "id" } );
            dynamic firstName = Fb.Get ( "/me", new { access_token = accessToken, fields = "first_name" } );
            dynamic lastName = Fb.Get ( "/me", new { access_token = accessToken, fields = "last_name" } );

            MyCustomLogging.Log ( "LAST NAME:\n" + lastName + "\nENDED" );
            MyCustomLogging.Log ( "FIRST NAME:\n" + firstName + "\nENDED" );

            string name = ( firstName + " " + lastName ).ToString ();
            int facebid = (int) fbid;

            using ( var db = new PetitionContext () )
            {
                var people =
                    db.People.Where ( person2 => person2.FacebookId.Equals ( facebid ) && person2.Name == name );
                var numberOfPeople = people.Count ();

                if ( numberOfPeople < 1 )
                {
                    var person = new Model.Person ()
                    {
                        FacebookId = facebid,
                        Name = name
                    };

                    db.People.Add ( person );
                    db.SaveChanges ();
                    return person.PersonId;
                }
                else
                {
                    if ( numberOfPeople >= 2 )
                        MyCustomLogging.Log ( "Something is wronhg" );

                    return people.First ().PersonId;
                }
            }
        }
    }
}