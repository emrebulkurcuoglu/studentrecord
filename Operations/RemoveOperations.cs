using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studenrecordsystem
{
    class RemoveOperations
    {
        public static void RemoveRecord()
        {
            Console.Write("\n>>>Remove Record");

            Console.Write("\n\nStudentId to remove: ");
            string studentIdToRemove = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
            SearchWebService.SearhWebServiceSoapClient client = new SearchWebService.SearhWebServiceSoapClient();
            int isRecord = client.FindRecord(studentIdToRemove);

            if (isRecord != -1)
            {
                Console.WriteLine("\n...Record cannot be found...\n");
                return;
            }
            else
            {

                SearchFindPrintOperations.PrintRecord(studentIdToRemove);

                Console.WriteLine("Are you sure Y/N: ");
                bool condition = true;

                while (condition)
                {
                    string yes_no = Console.ReadLine();

                    if (yes_no == "Y" || yes_no == "y")
                    {
                        ProgramSqlOperations.DeleteRecords(studentIdToRemove);
                        Console.WriteLine("\n...Record Removed...\n");
                        condition = false;
                    }
                    else if (yes_no == "N" || yes_no == "n")
                    {
                        Console.WriteLine("\n...Record Remove Canceled...\n");
                        condition = false;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Enter Y or N: ");
                    }
                }
            }
        }
    }
}
