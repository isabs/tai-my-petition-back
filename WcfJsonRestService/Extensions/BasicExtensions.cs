using System;

namespace WcfJsonRestService.Extensions
{
    public static class BasicExtensions
    {
        public static int ToInt ( this string a )
        {
            return Int32.Parse ( a );
        }
    }
}