using System;
using System.Collections.Generic;
using WcfJsonRestService.Extensions;
using WcfJsonRestService.Model;

namespace WcfJsonRestService.DB
{
    public class DataGenerator
    {
        private Person person1 = new Model.Person ()
        {
            FacebookId = "12334321",
            Name = "Henryk Kowalski"
        };
        private Person person2 = new Model.Person ()
        {
            FacebookId = "12335321",
            Name = "Marek Kowalski"
        };
        private Person person3 = new Model.Person ()
        {
            FacebookId = "12135321",
            Name = "Eustachy Kowalski"
        };
        private Person person4 = new Model.Person ()
        {
            FacebookId = "12137621",
            Name = "Kłapouchy Kowalski"
        };

        private Petition petition = new Model.Petition ()
        {
            Title = "Zwykla, wczoraj",
            Text = "Jakis przykladowy tekst 1",
            Url = @"http://www.focus.pl/upload/galleries/18/kot-18385_l.jpg",
            Addressee = "Kaczyński",
            Description = "Jakiś desc",
            CreatorId = 1,
            CreationDate = DateTime.Now.AddDays ( -1 ).GetUnixTime (),
            IsValid = true,
            Members = new List<Person> (),
            Tags = new List<Tag> ()
        };
        private Petition petition2 = new Model.Petition ()
        {
            Title = "Zwykla, dzis",
            Text = "Jakis przykladowy tekst 2",
            Url = @"http://www.focus.pl/upload/galleries/18/kot-18385_l.jpg",
            Addressee = "Tusk",
            Description = "Jakiś desc",
            CreatorId = 2,
            CreationDate = DateTime.Now.AddDays ( 1 ).GetUnixTime (),
            IsValid = true,
            Members = new List<Person> (),
            Tags = new List<Tag> ()
        };
        private Petition petition3 = new Model.Petition ()
        {
            Title = "Niewazna",
            Text = "Jakis przykladowy tekst 1",
            Url = @"http://www.focus.pl/upload/galleries/18/kot-18385_l.jpg",
            Addressee = "Kowalski",
            Description = "Jakiś desc",
            CreatorId = 1,
            CreationDate = DateTime.Now.AddDays ( -1 ).GetUnixTime (),
            IsValid = false,
            Members = new List<Person> (),
            Tags = new List<Tag> ()
        };
        private Tag tag1 = new Model.Tag ()
        {
            TagText = "tag1"
        };
        private Tag tag2 = new Model.Tag ()
        {
            TagText = "tag2"
        };
        private Tag tag3 = new Model.Tag ()
        {
            TagText = "tag3"
        };

        private void GeneratePetitions ( )
        {
            using ( var db = new PetitionContext () )
            {
                petition.Creator = person1;
                petition2.Creator = person2;
                petition.Creator = person1;

                petition.Members.Add ( person1 );
                petition.Members.Add ( person2 );
                petition.Tags.Add ( tag1 );
                petition.Tags.Add ( tag3 );

                petition2.Members.Add ( person2 );
                petition2.Members.Add ( person3 );
                petition2.Members.Add ( person1 );
                petition2.Tags.Add ( tag2 );

                petition3.Members.Add ( person1 );
                petition3.Members.Add ( person4 );
                petition3.Tags.Add ( tag3 );

                db.Petitions.Add ( petition );
                db.Petitions.Add ( petition2 );
                db.Petitions.Add ( petition3 );
                db.SaveChanges ();
            }
        }

        private void GenerateTags ( )
        {
            using ( var db = new PetitionContext () )
            {
                db.Tags.Add ( tag1 );
                db.Tags.Add ( tag2 );
                db.Tags.Add ( tag3 );
                db.SaveChanges ();
            }
        }

        private void GeneratePeople ( )
        {
            using ( var db = new PetitionContext () )
            {
                db.People.Add ( person1 );
                db.People.Add ( person2 );
                db.People.Add ( person3 );
                db.People.Add ( person4 );
                db.SaveChanges ();
            }
        }


        public void GenerateAll ( )
        {
            //GenerateTags ();
            //GeneratePeople ();
            GeneratePetitions ();
        }
    }
}