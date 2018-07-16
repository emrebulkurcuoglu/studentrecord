using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studenrecordsystem
{
    class Log
    {
        public static void writeToLogFile(string toWrite)
        {

            string path = ConfigurationManager.AppSettings["logPath"];
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path, true))
                {
                    file.WriteLine(toWrite);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
        }
    }
}
