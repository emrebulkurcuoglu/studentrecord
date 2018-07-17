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
            SearchWebService.SearhWebServiceSoapClient client = new SearchWebService.SearhWebServiceSoapClient();
            if (client.HowManyAdress(Id) == Constants.MAX_NUMBER_OF_ADRESS)
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
