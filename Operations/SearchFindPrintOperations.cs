using System;
using System.Data.SqlClient;
using static studenrecordsystem.Program;

namespace studenrecordsystem
{
    class SearchFindPrintOperations
    {
        public static void SearchRecord()
        {
            Console.Write("\n>>>Search Record");

            Console.Write("\n\nStudentId to search: ");
            string studentIdToSearch = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
            Log.writeToLogFile(DateTime.Now.ToString() + " Student with id " + studentIdToSearch + " id searched by " + LoginProcess.loginedUser.UserName);
            if (FindRecord(studentIdToSearch) != -1)
            {
                Console.WriteLine("Record cannot found...");
            }
            else
            {
                PrintRecord(studentIdToSearch);
            }
        }

        public static int FindRecord(string StudentIdToFindStudent)
        {
            try
            {
                string connectionString;
                SqlConnection sqlConnection;
                SqlCommand sqlCommand;

                connectionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string queryCount = "SELECT COUNT(StudentId) FROM [dbo].[Students] WHERE StudentId like '" + StudentIdToFindStudent + "'";
                sqlCommand = new SqlCommand(queryCount, sqlConnection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                read.Read();
                string recordCount = read[0].ToString();

                if (recordCount == "1")
                {
                    return -1;
                }
                sqlConnection.Close();
                return 0;
            }

            catch (Exception exception)
            {
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);
                return 0;
            }
        }

        public static void PrintRecord(string StudentIdToPrint)
        {
            try
            {
                string connectionString;
                SqlConnection sqlConnection;
                SqlCommand sqlCommand, sqlCommand2;

                connectionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                string querySelectStudent = "SELECT * FROM [dbo].[Students] WHERE StudentId like '" + StudentIdToPrint + "'";
                sqlCommand = new SqlCommand(querySelectStudent, sqlConnection);
                SqlDataReader read = sqlCommand.ExecuteReader();
                read.Read();

                int isThereRecord = FindRecord(StudentIdToPrint);
                if (isThereRecord != -1)
                {
                    Console.Write("Record is not found..");
                    sqlConnection.Close();
                    return;
                }
                else
                {
                    Console.Write("Name: ");
                    Console.WriteLine(read[0].ToString());
                    Console.Write("Surname: ");
                    Console.WriteLine(read[1].ToString());
                    Console.Write("Birthday: ");
                    Console.WriteLine(read[2].ToString());
                    Console.Write("Student Id: ");
                    Console.WriteLine(read[3].ToString());
                    Console.Write("Gsm: ");
                    Console.WriteLine(read[4].ToString());
                    Console.WriteLine("\nAdresses\n");
                }
                sqlConnection.Close();

                for (int i = 1; i <= AdressOperations.HowManyAdress(StudentIdToPrint); i++)
                {
                    sqlConnection.Open();
                    string querySelectAdress = "SELECT * FROM [dbo].[Adresses] WHERE StudentId like '" + StudentIdToPrint + "'" + "and AdressNo =" + i.ToString();
                    Console.WriteLine("\n\tAdresses" + i.ToString());
                    sqlCommand2 = new SqlCommand(querySelectAdress, sqlConnection);
                    SqlDataReader read3 = sqlCommand2.ExecuteReader();
                    read3.Read();
                    Console.Write("\t\tStreet: ");
                    Console.WriteLine(read3[2].ToString());
                    Console.Write("\t\tNeighbourhood: ");
                    Console.WriteLine(read3[3].ToString());
                    Console.Write("\t\tDistrict: ");
                    Console.WriteLine(read3[4].ToString());
                    Console.Write("\t\tState: ");
                    Console.WriteLine(read3[5].ToString());
                    sqlConnection.Close();
                }
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            catch (Exception exception)
            {
                Log.writeToLogFile(DateTime.Now.ToString() + " " + exception.Message);
                Console.WriteLine(exception.Message);

            }
        }
    }
}