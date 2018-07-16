using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studenrecordsystem.Operations.SqlOperations
{
    class GeneralSqlConnection
    {
        public static  SqlConnection CreateSqlConnection()
        {
            string connectionString;
            SqlConnection sqlConnection;

            connectionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
