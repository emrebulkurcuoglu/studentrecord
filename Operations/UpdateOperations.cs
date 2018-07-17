using System;

namespace studenrecordsystem
{
    class UpdateOperations
    {

        static class Constants
        {
            public const int MAX_NUMBER_OF_ADRESS = 3;
        }

        public static void UpdateRecord()
        {
            Console.Write("\n>>>Update Record");
            SearchWebService.SearhWebServiceSoapClient client = new SearchWebService.SearhWebServiceSoapClient();
            Console.Write("\n\nEnter Student Id to Update: ");
            string studentIdToUpdate = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
           
            int isRecord = client.FindRecord(studentIdToUpdate);
            if (isRecord != -1)
            {
                Console.WriteLine("\n...Record cannot be found...\n");
                return;
            }
            else
            {
                SearchFindPrintOperations.PrintRecord(studentIdToUpdate);
                Student studentToUpdate = new Student();
                studentToUpdate = StudentOperations.GetStudentInformations(false, studentIdToUpdate);

                ProgramSqlOperations.UpdateStudent(studentToUpdate, studentIdToUpdate);

                string choice = "-1";
                while (choice != "9")
                {
                    Console.WriteLine("\nEnter Adress Number To Update || 9-EXIT");
                    choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        AdressOperations.UpdateAddresses(studentIdToUpdate, 1);
                    }
                    else if (client.HowManyAdress(studentIdToUpdate) < Convert.ToInt32(choice) && Convert.ToInt32(choice) <= Constants.MAX_NUMBER_OF_ADRESS)
                    {
                        Console.Write("\nThere is no adress" + choice + ". Please enter valid address number or press 0 to add new adress");
                        choice = Console.ReadLine();
                        if (choice == "0")
                        {
                            AdressOperations.AddAlternativeAddresses(studentIdToUpdate);
                        }
                    }
                    else if (client.HowManyAdress(studentIdToUpdate) > Convert.ToInt32(choice)-1 && Convert.ToInt32(choice) <= Constants.MAX_NUMBER_OF_ADRESS)
                    {
                        AdressOperations.UpdateAddresses(studentIdToUpdate, Convert.ToInt32(choice));
                    }
                    else if(choice == "9")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nPlease enter valid number...");
                    }
                    
                }
                Console.WriteLine("...Record Updated...");
            }

        }
    }
}
