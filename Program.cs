﻿using log4net;
using studenrecordsystem.Login;
using studenrecordsystem.LoginSignUp;
using studenrecordsystem.Model;
using System;
using System.Windows.Forms;

namespace studenrecordsystem
{
    class Program
    {
        public static class LogOperation
        {
            public static ILog LogProgram = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); 
        }
        public static class LoginProcess
        {
            public static User loginedUser;
            public static DateTime loginDate;
            public static DateTime logOutDate;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("***STUDENT RECORD SYSTEM***\n");

            LoginMenu();
            return;
        }

        static void LoginMenu()
        {
            LogOperation.LogProgram.Info("Program Started.");
            string choice = "-1";
            while (choice != "9")
            {
                Console.WriteLine("\n<<<<< Please Choose One The Operation (1-LOGIN | 2-SIGNUP | 9-EXIT) >>>>>");
                Console.Write("\nOperation>> ");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":

                        LoginControl.LoginAccessControl();
                        break;

                    case "2":
                        SignUp.SignUpUser();
                        break;

                    case "9":
                        LogOperation.LogProgram.Info("Program Finished.");
                        Console.WriteLine("...Exit...");
                        break;

                    default:
                        Console.WriteLine("Please Enter Valid Operation Number\n");
                        break;
                }

            }

        }

        public static void RecordSystemMenu()
        {

            Console.WriteLine("Welcome " + LoginProcess.loginedUser.Name + " " + LoginProcess.loginedUser.Surname);
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
                        LoginProcess.logOutDate = DateTime.Now;
                        LogOperation.LogProgram.Info( LoginProcess.loginedUser.UserName + " logout.");
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
