using studenrecordsystem.Operations.SqlOperations;
using System;
using System.Data.SqlClient;
using static studenrecordsystem.Program;

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
                Log.writeToLogFile(DateTime.Now.ToString() + " Student with " + studentToInsert.StudentId + " id is added by " + LoginProcess.loginedUser.UserName);
                sqlConnection.Close();
            }

            catch (Exception exception)
            {
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);
            }
        }

        public static void DeleteRecords(string studentIdToDelete)
        {
            SqlCommand sqlCommand;

            SqlConnection sqlConnection = GeneralSqlConnection.CreateSqlConnection();


            try
            {
                string query1 = "DELETE FROM dbo.Students WHERE StudentId like " + studentIdToDelete + ";\n";
                query1 = query1 + "DELETE FROM dbo.Adresses WHERE StudentId like " + studentIdToDelete + ";\n";

                sqlCommand = new SqlCommand(query1, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Log.writeToLogFile(DateTime.Now.ToString() + " Student with " + studentIdToDelete + " id is deleted by " + LoginProcess.loginedUser.UserName);

            }
            catch (Exception exception)
            {
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);
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
                queryToInsertAdress = queryToInsertAdress + "(" + "\n'" + StudentIdToInsertAdress + "'" + "\n,'" + (AdressOperations.HowManyAdress(StudentIdToInsertAdress) + 1).ToString() + "'" + "\n,'" + adressToInsert.Street + "'" + "\n,'" + adressToInsert.Neighborhood + "'" + "\n,'" + adressToInsert.District + "'" + "\n,'" + adressToInsert.State + "'\n,'" + Program.LoginProcess.loginedUser.UserName + "'\n,'" + Program.LoginProcess.loginedUser.UserName + "')";
                sqlCommand = new SqlCommand(queryToInsertAdress, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                Log.writeToLogFile(DateTime.Now.ToString() + " Adress added to student with " + StudentIdToInsertAdress + " id by " + LoginProcess.loginedUser.UserName);
                sqlConnection.Close();
                return;
            }
            catch (Exception exception)
            {
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);
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
                Log.writeToLogFile(DateTime.Now.ToString() + " Student with " + studentIdToUpdate + " id is updated by " + LoginProcess.loginedUser.UserName);
                sqlConnection.Close();
                return;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
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
                Log.writeToLogFile(DateTime.Now.ToString() + " Adress updated, student with " + studentIdToUpdate + " id by " + LoginProcess.loginedUser.UserName);
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
            }
        }
    }
}
