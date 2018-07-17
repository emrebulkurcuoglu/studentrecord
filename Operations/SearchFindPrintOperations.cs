using System;
using System.Data.SqlClient;
using static studenrecordsystem.Program;

namespace studenrecordsystem
{
    class SearchFindPrintOperations
    {
        public static void SearchRecord()
        {
            Console.Write("\n>>>Search Record");

            Console.Write("\n\nStudentId to search: ");
            string studentIdToSearch = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
            SearchWebService.SearhWebServiceSoapClient client = new SearchWebService.SearhWebServiceSoapClient();
            Log.writeToLogFile(DateTime.Now.ToString() + " Student with id " + studentIdToSearch + " id searched by " + LoginProcess.loginedUser.UserName);
            if (client.FindRecord(studentIdToSearch) != -1)
            {
                Console.WriteLine("Record cannot found...");
            }
            else
            {
                PrintRecord(studentIdToSearch);
            }
        }

        public static void PrintRecord(string studentIdToPrint)
        {
            try
            {
                SearchWebService.SearhWebServiceSoapClient client = new SearchWebService.SearhWebServiceSoapClient();
                SearchWebService.Student studentToPrint = new SearchWebService.Student();
                studentToPrint = client.GetStudent(studentIdToPrint);


                int isThereRecord = client.FindRecord(studentToPrint.StudentId);
                if (isThereRecord != -1)
                {
                    Console.Write("Record is not found..");
                    return;
                }
                else
                {
                    Console.Write("Name: ");
                    Console.WriteLine(studentToPrint.Name);
                    Console.Write("Surname: ");
                    Console.WriteLine(studentToPrint.Surname);
                    Console.Write("Birthday: ");
                    Console.WriteLine(studentToPrint.Birthday.ToString().Substring(0,10));
                    Console.Write("Student Id: ");
                    Console.WriteLine(studentToPrint.StudentId);
                    Console.Write("Gsm: ");
                    Console.WriteLine(studentToPrint.Gsm);
                    Console.WriteLine("\nAdresses\n");
                }
                
                for (int i = 1; i <= client.HowManyAdress(studentIdToPrint); i++)
                {

                    SearchWebService.Adress adressToPrint = new SearchWebService.Adress();
                    adressToPrint = client.GetAdress(studentIdToPrint, i);
                    Console.WriteLine("\n\tAdresses" + i.ToString());

                    Console.Write("\t\tStreet: ");
                    Console.WriteLine(adressToPrint.Street);
                    Console.Write("\t\tNeighbourhood: ");
                    Console.WriteLine(adressToPrint.Neighborhood);
                    Console.Write("\t\tDistrict: ");
                    Console.WriteLine(adressToPrint.District);
                    Console.Write("\t\tState: ");
                    Console.WriteLine(adressToPrint.State);

                }

            }

            catch (Exception exception)
            {
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);
            }
        }
    }
}