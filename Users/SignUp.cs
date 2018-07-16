using studenrecordsystem.Model;
using studenrecordsystem.Operations.SqlOperations;
using System;

namespace studenrecordsystem.LoginSignUp
{
    class SignUp
    {
        public static void SignUpUser()
        {
            User userToSignUp = new User();
            
            Console.Write("Name: ");
            userToSignUp.Name = Console.ReadLine();

            Console.Write("Surname: ");
            userToSignUp.Surname = Console.ReadLine();

            Console.Write("Username: ");
            userToSignUp.UserName = AddingUsername();

            Console.Write("Password: ");
            userToSignUp.UserPassword = Console.ReadLine();

            Console.Write("Gsm: ");
            userToSignUp.Gsm = Utils.GetNumericValueWithValidation(Console.ReadLine(), "Gsm: ", "\nPlease Enter Valid Gsm with these format (5#########)\n\n", true, 10);

            UserSqlOperations.InsertUser(userToSignUp);
            Program.LoginProcess.loginedUser = UserSqlOperations.GetUser(userToSignUp.UserName);
            Program.LoginProcess.loginDate = DateTime.Now;
            Log.writeToLogFile(DateTime.Now.ToString() + " " + userToSignUp.UserName + " signed up.");
            Program.RecordSystemMenu();
        }

        public static string AddingUsername()
        {
            User userToCheck = new User();
            userToCheck.UserName = Console.ReadLine();
            bool isUniqueUsername = true;
            while (isUniqueUsername)
            {
                if (UserSqlOperations.IsValidUser(userToCheck, false))
                {
                    Console.WriteLine("...This UserName Has Already Taken..\n\n");
                    isUniqueUsername = true;
                    Console.Write("Username: ");
                    userToCheck.UserName = Console.ReadLine();
                }
                else
                {
                    isUniqueUsername = false;
                }

            }

            return userToCheck.UserName;
        }
        
    }
}
