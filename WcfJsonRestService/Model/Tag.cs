using System.Collections.Generic;

namespace WcfJsonRestService.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagText { get; set; }

        public ICollection<Petition> PetitionsWithTag { get; set; }
    }
}