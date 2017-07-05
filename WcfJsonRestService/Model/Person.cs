using System.Collections.Generic;

namespace WcfJsonRestService.Model
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FacebookId { get; set; }
        public string Name { get; set; }

        public ICollection<Petition> OwnedPetitions { get; set; }
        public ICollection<Petition> SignedPetitions { get; set; }
    }
}