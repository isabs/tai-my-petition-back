using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
        public class GetAllPetitions()
        {
            
        }

    }
}
