using System;
using System.IO;

namespace WcfJsonRestService
{
    public static class MyCustomLogging
    {
        public static void Log ( string text )
        {
            var file = new StreamWriter ( @"C:\utils\mylog.txt", true );
            file.WriteLine ( $"[{DateTime.Now.ToString ( "hhmmss.ffff" )}] {text}\n" );
            file.Close ();
            //Console.WriteLine ( text );
        }

        public static void Log ( string text, string arg )
        {
            var file = new StreamWriter ( @"C:\utils\mylog.txt", true );
            file.WriteLine ( $"[{DateTime.Now.ToString ( "hhmmss.ffff" )}] " + string.Format ( text, arg ) + "\n" );
            file.Close ();
            //Console.WriteLine ( text, arg );
        }

    }
}