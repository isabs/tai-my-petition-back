using System.Collections.Generic;

namespace WcfJsonRestService.WebModel
{
    public class RequestBody
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
    }
}