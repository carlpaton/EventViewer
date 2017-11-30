using System;

namespace EvtxImporter
{
    class Program
    {
        /// <summary>
        /// Large event files (10mb+) take time to process 
        /// So I used a console application instead of a web project
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //run the image with 'Admin' privileges to create event log on local OS
            //EventLogger.Create(null, null); 

            new CheckFileLocation().Go();

            Console.WriteLine("DONE");
            Console.Read();
        }
    }
}
