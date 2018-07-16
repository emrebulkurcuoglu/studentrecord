using studenrecordsystem.Model;
using System;
using System.Data.SqlClient;

namespace studenrecordsystem.Operations.SqlOperations
{
    class UserSqlOperations
    {
        public static bool IsValidUser(User userToLoginControl,bool check)
        {
            try
            {
                SqlCommand sqlCommand;
                SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();
                string queryToLoginControl = "";
                if (check)
                {
                    queryToLoginControl = "SELECT COUNT(UserName) FROM [dbo].[Users] WHERE UserName like '" + userToLoginControl.UserName + "' and UserPasword like '" + userToLoginControl.UserPassword + "';";
                }
                else
                {
                    queryToLoginControl = "SELECT COUNT(UserName) FROM [dbo].[Users] WHERE UserName like '" + userToLoginControl.UserName + "';";
                }

                sqlCommand = new SqlCommand(queryToLoginControl, sqlConnection);

                SqlDataReader read2 = sqlCommand.ExecuteReader();
                read2.Read();
                string recordCount2 = read2[0].ToString();

                if (recordCount2 == "1")
                {
                    sqlConnection.Close();
                    return true;
                }
                else
                {
                    sqlConnection.Close();
                    return false;
                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }

        }

        public static void InsertUser(User userToSignUp)
        {
            try
            {
                SqlCommand sqlCommand;
                SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();

                string queryInsertStudent = "INSERT INTO [dbo].[Users] \n([Name],\n[Surname],\n[UserName],\n[UserPasword],\n[GSM])\n VALUES";
                queryInsertStudent = queryInsertStudent + "(" + "\n'" + userToSignUp.Name + "'" + "\n,'" + userToSignUp.Surname + "'" + "\n,'" + userToSignUp.UserName + "'" + "\n,'" + userToSignUp.UserPassword + "'" + "\n,'" + userToSignUp.Gsm + "')";
                sqlCommand = new SqlCommand(queryInsertStudent, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static User GetUser(string userNameToGet)
        {
            try
            {
                User userToReturn = new User();
                SqlCommand sqlCommand;
                SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();

                string queryToLoginControl = "SELECT * FROM [dbo].[Users] WHERE UserName like '" + userNameToGet + "';";

                sqlCommand = new SqlCommand(queryToLoginControl, sqlConnection);

                SqlDataReader read2 = sqlCommand.ExecuteReader();
                read2.Read();
                userToReturn.Name= read2[0].ToString();
                userToReturn.Surname = read2[1].ToString();
                userToReturn.UserName = read2[2].ToString();
                userToReturn.UserPassword = read2[3].ToString();
                userToReturn.Gsm = read2[4].ToString();

                return userToReturn;
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }

        }
    }
}
