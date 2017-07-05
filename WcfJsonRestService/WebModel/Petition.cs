using System;
using System.Collections.Generic;

namespace WcfJsonRestService.WebModel
{
    public class PetitionShort
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Addressee { get; set; }
        public DateTime CreationDate { get; set; }
        public int SignCount { get; set; }
        public List<string> Tags { get; set; }
        public Person Owner { get; set; }
    }

    public class PetitionNormal : PetitionShort
    {
        public string Description { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public List<Person> Signs { get; set; }
    }
}