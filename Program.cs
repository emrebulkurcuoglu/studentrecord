using System;
using System.Collections.Generic;

namespace studenrecordsystem
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Student> students = new List<Student>();

            /*if (!Operations.readFromXml(students))
            {
                Console.ReadKey();
                return;
            }*/

            Console.WriteLine("***STUDENT RECORD SYSTEM***\n");
            string choice = "-1";
            while (choice != "9")
            {
                Console.WriteLine("\n<<<<< Please Choose One The Operation (1-ADD | 2-UPDATE | 3-REMOVE | 4-SEARCH | 9-EXIT) >>>>>");
                Console.Write("\nOperation>> ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Operations.AddRecord(students);
                        break;

                    case "2":
                        Operations.UpdateRecord(students);
                        break;

                    case "3":
                        Operations.RemoveRecord(students);
                        break;

                    case "4":
                        Operations.SearchRecord(students);
                        break;

                    case "9":
                        Console.WriteLine("...Exit...");
                        break;

                    default:
                        Console.WriteLine("Please Enter Valid Operation Number\n");
                        break;
                }

            }

            /*if (!Operations.WriteToXml(students))
            {
                Console.ReadKey();
                return;
            }*/

        }

    }
}
