using System.Collections.Specialized;
using System.Web;

namespace WcfJsonRestService
{
    public class HeaderTools
    {
        public string GetAccesKey ( )
        {
            HttpContext httpContext = HttpContext.Current;
            NameValueCollection headerList = httpContext.Request.Headers;
            return headerList.Get ( "Authorization" );
        }
    }
}