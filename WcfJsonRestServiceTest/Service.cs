using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WcfJsonRestService;
using WcfJsonRestService.DB;
using WcfJsonRestService.Extensions;

namespace WcfJsonRestServiceTest
{
    [TestFixture]
    public class Service
    {
        public HttpClient HttpClient { get; set; }

        [SetUp]
        public void SetUp ( )
        {
            HttpClient = new HttpClient ();


        }

        [Test]
        public void GetAllPetitions ( )
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

    }
}
