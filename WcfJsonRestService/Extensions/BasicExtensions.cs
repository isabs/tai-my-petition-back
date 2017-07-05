using System;
using System.Data.Entity;

namespace WcfJsonRestService.Extensions
{
    public static class BasicExtensions
    {
        public static int ToInt ( this string a )
        {
            return Int32.Parse ( a );
        }

        public static void Clear<T> ( this DbSet<T> dbSet ) where T : class
        {
            dbSet.RemoveRange ( dbSet );
        }

        public static long GetUnixTime ( this DateTime date )
        {
            long epochTicks = new DateTime ( 1970, 1, 1 ).Ticks;
            return ( ( date.Ticks - epochTicks ) / TimeSpan.TicksPerSecond );
        }
    }
}