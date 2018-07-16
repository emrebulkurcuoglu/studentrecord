using System;

namespace studenrecordsystem
{
    class AddOperations
    {
        public static void AddRecord()
        {
            Console.WriteLine("\n>>>Adding Record");
            Console.WriteLine("\nPlease Enter Necessary Informations\n");

            Student toAddStudent = new Student();
            toAddStudent = StudentOperations.GetStudentInformations(true, "");
            if (toAddStudent.StudentId == "false")
            {
                return;
            }

            AdressOperations.AddAlternativeAddresses(toAddStudent.StudentId);
            
            bool condition = true;

            while (condition)
            {
                Console.WriteLine("Dou you want to add more adress Y/N: ");
                string yes_no = Console.ReadLine();

                if (yes_no == "Y" || yes_no == "y")
                {
                    bool status = AdressOperations.AddAlternativeAddresses(toAddStudent.StudentId);
                    if (status)
                    {
                        condition = true;
                    }
                    else
                    {
                        condition = false;
                    }

                }
                else if (yes_no == "N" || yes_no == "n")
                {
                    condition = false;
                }
                else
                {
                    Console.WriteLine("\nPlease Enter Y or N: ");
                }
            }

            ProgramSqlOperations.InsertStudent(toAddStudent);

            Console.WriteLine("\n...Added...\n");
        }

        public static string AddingId()
        {
            string toReturnStudentId = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
            bool isUniqueId = true;
            while (isUniqueId)
            {
                if (SearchFindPrintOperations.FindRecord(toReturnStudentId) == -1)
                {
                    Console.WriteLine("...This Student Id Has Already Recorded...\n\n");

                    SearchFindPrintOperations.PrintRecord(toReturnStudentId);
                    Console.WriteLine("\n...Do You Want To Enter Another Id (Y/N)...");

                    bool condition = true;
                    while (condition)
                    {
                        string yes_no = Console.ReadLine();

                        if (yes_no == "Y" || yes_no == "y")
                        {
                            Console.Write("Student Id: ");
                            toReturnStudentId = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
                            condition = false;
                        }
                        else if (yes_no == "N" || yes_no == "n")
                        {
                            toReturnStudentId = "false";
                            condition = false;
                            return toReturnStudentId;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter Y or N: ");
                        }
                    }

                }
                else
                {
                    isUniqueId = false;
                }

            }

            return toReturnStudentId;
        }
    }
}
