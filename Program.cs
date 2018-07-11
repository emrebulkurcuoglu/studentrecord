﻿using System;

namespace studenrecordsystem
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        AddOperations.AddRecord();
                        break;

                    case "2":
                        UpdateOperations.UpdateRecord();
                        break;

                    case "3":
                        RemoveOperations.RemoveRecord();
                        break;

                    case "4":
                        SearchFindPrintOperations.SearchRecord();
                        break;

                    case "9":
                        Console.WriteLine("...Exit...");
                        break;

                    default:
                        Console.WriteLine("Please Enter Valid Operation Number\n");
                        break;
                }

            }

        }
    }
}
