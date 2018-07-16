using System;
using System.Data.SqlClient;

namespace studenrecordsystem
{
    static class Constants
    {
        public const int MAX_NUMBER_OF_ADRESS = 3;
    }

    class AdressOperations
    {
        public static bool AddAlternativeAddresses(string Id)
        {
            SearchFindPrintOperations.FindRecord(Id);

            if (HowManyAdress(Id) == Constants.MAX_NUMBER_OF_ADRESS)
            {
                Console.WriteLine("You Have Already Add Three Adress. You Cannot Add More Adress Information");
                return false;
            }
            else
            {
                ProgramSqlOperations.InsertAdress(GetAdress(), Id);
                return true;
            }
        }

        public static int HowManyAdress(string id)
        {
            string connetionString;
            SqlConnection cnn;
            SqlCommand cmd2;

            connetionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            string query2 = "SELECT COUNT(StudentId) FROM [dbo].[Adresses] WHERE StudentId like '" + id + "'";
            cmd2 = new SqlCommand(query2, cnn);
            SqlDataReader read2 = cmd2.ExecuteReader();
            read2.Read();
            string recordCount2 = read2[0].ToString();

            cnn.Close();

            return Convert.ToInt32(recordCount2);
        }

        public static void UpdateAddresses(string studentIdToUpdate, int adressNoToUpdate)
        {
            Adress adressToUpdate = new Adress();
            adressToUpdate = GetAdress();

            ProgramSqlOperations.UpdateAdress(adressToUpdate, studentIdToUpdate, adressNoToUpdate);
        }

        public static Adress GetAdress()
        {
            Adress adressToAdd = new Adress();
            Console.WriteLine("Please Enter Adress Informations: ");

            Console.Write("Street: ");
            adressToAdd.Street = Console.ReadLine();

            Console.Write("Neighborhood: ");
            adressToAdd.Neighborhood = Console.ReadLine();

            Console.Write("District: ");
            adressToAdd.District = Console.ReadLine();

            Console.Write("State: ");
            adressToAdd.State = Console.ReadLine();

            return adressToAdd;
        }
    }
}
