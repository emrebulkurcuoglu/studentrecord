﻿using studenrecordsystem.Model;
using studenrecordsystem.Operations.SqlOperations;
using System;
using static studenrecordsystem.Program;

namespace studenrecordsystem.Login
{
    class LoginControl
    {
        public static void LoginAccessControl()
        {
            User userToLogin = new User();
            userToLogin.Name = "";
            userToLogin.Surname = "";

            Console.Write("Username: ");
            userToLogin.UserName = Console.ReadLine();

            Console.Write("Password: ");
            userToLogin.UserPassword = Console.ReadLine();

            userToLogin.Gsm = "";

            if (UserSqlOperations.IsValidUser(userToLogin, true))
            {
                LoginProcess.loginedUser = UserSqlOperations.GetUser(userToLogin.UserName);
                LoginProcess.loginDate = DateTime.Now;

                LogOperation.LogProgram.Info(LoginProcess.loginedUser.UserName + " logined.");
                RecordSystemMenu();
            }
            else
            {
                LogOperation.LogProgram.Info(userToLogin.UserName + " has entered invalid username or password.");
                
                Console.WriteLine("Invalid user name or password...");
            }
        }
    }
}
