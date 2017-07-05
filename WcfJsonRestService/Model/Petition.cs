using System;
using System.Collections.Generic;

namespace WcfJsonRestService.Model
{
    public class Petition
    {
        public int PetitionId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string Addressee { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; } //foreign key -> Person
        public long CreationDate { get; set; }
        public bool IsValid { get; set; }


        public virtual ICollection<Tag> Tags { get; set; }
        public virtual Person Creator { get; set; }
        public virtual ICollection<Person> Members { get; set; }


        /*        public Petition ( )
                {
                    Tags = new List<Tag> ();
                    Members = new List<Person> ();
                }*/
    }
}