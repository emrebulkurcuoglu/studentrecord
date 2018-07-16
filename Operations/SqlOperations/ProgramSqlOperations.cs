using studenrecordsystem.Operations.SqlOperations;
using System;
using System.Data.SqlClient;

namespace studenrecordsystem
{
    class ProgramSqlOperations
    {
        public static void InsertStudent(Student studentToInsert)
        {
            try
            {
                SqlCommand sqlCommand;
                SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();

                string queryInsertStudent = "INSERT INTO [dbo].[Students] \n([SName],\n[Surname],\n[Birthday],\n[StudentId],\n[GSM],\n[CreatedBy],\n[LastUpdatedBy])\n VALUES";
                queryInsertStudent = queryInsertStudent + "(" + "\n'" + studentToInsert.Name + "'" + "\n,'" + studentToInsert.Surname + "'" + "\n,'" + Utils.GetDateTimeOnConsoleWithValidationAndFormat(studentToInsert.Birthday.ToString().Substring(0, 10), "", "").ToString().Substring(0, 10) + "'" + "\n,'" + studentToInsert.StudentId + "'" + "\n,'" + studentToInsert.Gsm + "'\n,'"+Program.LoginProcess.loginedUser.UserName + "'\n,'" + Program.LoginProcess.loginedUser.UserName + "')";
                sqlCommand = new SqlCommand(queryInsertStudent, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void DeleteRecords(string studentIdToDelete)
        {
            SqlCommand sqlCommand;

            SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();
            sqlConnection.Open();

            try
            {
                string query1 = "DELETE FROM dbo.Students WHERE StudentId like " + studentIdToDelete + ";\n";
                query1 = query1 + "DELETE FROM dbo.Adresses WHERE StudentId like " + studentIdToDelete + ";\n";

                sqlCommand = new SqlCommand(query1, sqlConnection);
                sqlCommand.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            sqlConnection.Close();
        }

        public static void InsertAdress(Adress adressToInsert, string StudentIdToInsertAdress)
        {
            try
            {
                SqlCommand sqlCommand;
                SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();
                string queryToInsertAdress = "INSERT INTO [dbo].[Adresses] \n([StudentId],\n[AdressNo],\n[Street],\n[Neighbourhood],\n[District],\n[State_],\n[CreatedBy],\n[LastUpdatedBy])\n VALUES";
                //queryToInsertAdress = queryToInsertAdress + "(" + "\n'" + StudentIdToInsertAdress + "'" + "\n,'" + (AdressOperations.HowManyAdress(StudentIdToInsertAdress) + 1).ToString() + "'" + "\n,'" + adressToInsert.Street + "'" + "\n,'" + adressToInsert.Neighborhood + "'" + "\n,'" + adressToInsert.District + "'" + "\n,'" + adressToInsert.State + "'\n,'" + Program.LoginProcess.loginedUser.UserName + "'\n,'" + Program.LoginProcess.loginedUser.UserName + "')";
                sqlCommand = new SqlCommand(queryToInsertAdress, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return;
            }
        }

        public static void UpdateStudent(Student studentToUpdate, string studentIdToUpdate)
        {
            try
            {

                SqlCommand sqlCommand;

                SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();

                string queryToUpdateAdress = "UPDATE dbo.Students SET [SName] = '" + studentToUpdate.Name + "',\n [Surname] = '" + studentToUpdate.Surname + "',\n [Birthday] = '" + Utils.GetDateTimeOnConsoleWithValidationAndFormat(studentToUpdate.Birthday.ToString().Substring(0, 10), "", "").ToString().Substring(0, 10) + "',\n [StudentId] = '" + studentToUpdate.StudentId + "'\n, [Gsm] = '" + studentToUpdate.Gsm + "'\n,";
                queryToUpdateAdress = queryToUpdateAdress + " [LastUpdatedBy] = '" + Program.LoginProcess.loginedUser.UserName + "'\n";
                queryToUpdateAdress = queryToUpdateAdress + "WHERE StudentId like " + studentIdToUpdate + ";";
                sqlCommand = new SqlCommand(queryToUpdateAdress, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                return;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return;
            }
        }

        public static void UpdateAdress(Adress adressToUpdate, string studentIdToUpdate, int adressNoToUpdate)
        {
            try
            {
                SqlCommand sqlCommand;

                SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();
                string queryToUpdateAdress = "UPDATE dbo.Adresses SET [Street] = '" + adressToUpdate.Street + "',\n [Neighbourhood] = '" + adressToUpdate.Neighborhood + "',\n [District] = '" + adressToUpdate.District + "',\n [State_] = '" + adressToUpdate.State + "'\n,";
                queryToUpdateAdress = queryToUpdateAdress + " [LastUpdatedBy] = '" + Program.LoginProcess.loginedUser.UserName + "'\n";
                queryToUpdateAdress = queryToUpdateAdress + "WHERE StudentId like " + studentIdToUpdate + " and AdressNo = " + adressNoToUpdate + ";";
                sqlCommand = new SqlCommand(queryToUpdateAdress, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
