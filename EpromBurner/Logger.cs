using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EpromBurner
{
    class Logger
    {
        private static string logFilePath; // path of the log file 
        private static StreamWriter loggerStremWriter = null; // initiate the StreamWriter
        
        /// <summary>
        /// create a log file to save all actions taken by the device
        /// </summary>
        /// <param name="path"> the path of the created file </param>
        /// <returns> true if the file was created, false otherwise </returns>
        public static bool CreateLoggerFile ( string path )
        {
            bool result = true;
            try
            {

                logFilePath = path; 
                loggerStremWriter = new StreamWriter(path, true); // open a new stream writer

            }
            catch(Exception e)
            {
                if (e is IOException || e is NotSupportedException)
                {
                    Console.WriteLine("An error occurred while creating the file.", e);
                }
                result = false;
                
            }
            return result;
        }

        /// <summary>
        /// write to the log file the current time and the action/data  
        /// </summary>
        /// <param name="line"> the line to add to the log file </param>
        public static void WriteToLog (String line)
        {
            try
            {
                loggerStremWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss") + "." + DateTime.Now.Millisecond.ToString("D3") + " - " + line);
                loggerStremWriter.Flush();
            }
            catch(IOException e)
            {
                Console.WriteLine("unable to write to file", e);
            }
        }

    }
}
