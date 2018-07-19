using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static studenrecordsystem.Program;

namespace studenrecordsystem.Operations.SqlOperations
{
    class GeneralSqlConnection
    {
        public static  SqlConnection CreateSqlConnection()
        {
            string connectionString;
            SqlConnection sqlConnection;

            connectionString = ConfigurationManager.AppSettings["databaseString"];
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                return sqlConnection;
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                LogOperation.LogProgram.Error(exception.Message);
                return null;
            }
        }
    }
}
