using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace studenrecordsystem
{
    class Operations
    {
        static class Constants
        {
            public const int MAX_NUMBER_OF_ADRESS = 3;
        }

        public static void AddRecord()
        {
            Console.WriteLine("\n>>>Adding Record");
            Console.WriteLine("\nPlease Enter Necessary Informations\n");

            Student toadd = new Student();
            toadd.Adress = new List<AdressClass>();
            
            Console.Write("Name: ");
            toadd.Name = Console.ReadLine();

            Console.Write("Surname: ");
            toadd.Surname = Console.ReadLine();

            Console.Write("Birthday(dd.MM.yyyy): ");
            toadd.Birthday = Utils.GetDateTimeOnConsoleWithValidationAndFormat(Console.ReadLine(), "\nBirthday(dd.MM.yyyy): ", "Please Enter Valid Birthday with these format (dd.MM.yyyy)\n\n");


            Console.Write("StudentId: ");
            string temp = AddingId();

            if (temp == "false")
            {
                return;
            }
            else
            {
                toadd.StudentId = temp;
            }

            Console.Write("Gsm: ");
            toadd.Gsm = Utils.GetNumericValueWithValidation(Console.ReadLine(), "Gsm: ", "\nPlease Enter Valid Gsm with these format (5#########)\n\n", true, 10);

            AddAlternativeAddresses(toadd.StudentId);

            bool condition = true;

            while (condition)
            {
                Console.WriteLine("Dou you want to add more adress Y/N: ");
                string yes_no = Console.ReadLine();

                if (yes_no == "Y" || yes_no == "y")
                {
                    AddAlternativeAddresses(toadd.StudentId);
                    condition = true;
                }
                else if (yes_no == "N" || yes_no == "n")
                {
                    condition = false;
                }
                else
                {
                    Console.WriteLine("\nPlease Enter Y or N: ");
                }
            }

            try
            {
                string connetionString;
                SqlConnection cnn;
                SqlCommand cmd;

                connetionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
      
                string query2 = "INSERT INTO [dbo].[Students] \n([SName],\n[Surname],\n[Birthday],\n[StudentId],\n[GSM])\n VALUES";
                query2 = query2 + "(" + "\n'" + toadd.Name + "'" + "\n,'" + toadd.Surname + "'" + "\n,'" + Utils.GetDateTimeOnConsoleWithValidationAndFormat(toadd.Birthday.ToString().Substring(0, 10), "", "").ToString().Substring(0, 10) + "'" + "\n,'" +toadd.StudentId + "'" + "\n,'" + toadd.Gsm + "')";
                cmd = new SqlCommand(query2, cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("\n...Added...\n");
        }

        public static void AddAlternativeAddresses(string Id)
        {
            FindRecord(Id);
            
            try
            {
                string connetionString;
                SqlConnection cnn;
                SqlCommand cmd;

                connetionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                string query1 = "SELECT COUNT(StudentId) FROM [dbo].[Adresses] WHERE StudentId like '" + Id + "'";
                cmd = new SqlCommand(query1, cnn);
                SqlDataReader read = cmd.ExecuteReader();
                read.Read();
                string recordCount = read[0].ToString();
                cnn.Close();
                cnn.Open();

                if (Convert.ToUInt32(recordCount) == Constants.MAX_NUMBER_OF_ADRESS)
                {
                    Console.WriteLine("You Have Already Add Three Adress. You Cannot Add More Adress Information");
                }
                else
                {
                    AdressClass adresstoadd2 = new AdressClass();
                    Console.WriteLine("Please Enter Adress Informations: ");

                    Console.Write("Street: ");
                    adresstoadd2.Street = Console.ReadLine();

                    Console.Write("Neighborhood: ");
                    adresstoadd2.Neighborhood = Console.ReadLine();

                    Console.Write("District: ");
                    adresstoadd2.District = Console.ReadLine();

                    Console.Write("State: ");
                    adresstoadd2.State = Console.ReadLine();
                    string query2 = "INSERT INTO [dbo].[Adresses] \n([StudentId],\n[AdressNo],\n[Street],\n[Neighbourhood],\n[District],\n[State_])\n VALUES";
                    query2 = query2 + "(" + "\n'" + Id + "'" + "\n,'" + (recordCount + 1).ToString() + "'" + "\n,'" + adresstoadd2.Street + "'" + "\n,'" + adresstoadd2.Neighborhood + "'" + "\n,'" + adresstoadd2.District + "'" + "\n,'" + adresstoadd2.State + "')";
                    cmd = new SqlCommand(query2, cnn);
                    cmd.ExecuteNonQuery();
                }

                cnn.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void UpdateAddresses(List<Student> student, string Id, int indexAdress)
        {
            Console.WriteLine("Please Enter Adress Informations: ");

            Console.Write("Street: ");
            student[0].Adress[indexAdress].Street = Console.ReadLine();

            Console.Write("Neighborhood: ");
            student[0].Adress[indexAdress].Neighborhood = Console.ReadLine();

            Console.Write("District: ");
            student[0].Adress[indexAdress].District = Console.ReadLine();

            Console.Write("State: ");
            student[0].Adress[indexAdress].State = Console.ReadLine();

        }

        public static void UpdateRecord(List<Student> student)
        {
            Console.Write("\n>>>Update Record");

            Console.Write("\n\nEnter Student Id to Update: ");
            string id = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);

            int index_to_update = 0;//= FindRecord(student, id);
            if (index_to_update == -1)
            {
                Console.WriteLine("\n...Record cannot be found...\n");
                return;
            }
            else
            {
                PrintRecord("");

                Console.Write("Name: ");
                student[index_to_update].Name = Console.ReadLine();

                Console.Write("Surname: ");
                student[index_to_update].Surname = Console.ReadLine();

                Console.Write("Birthday(dd.MM.yyyy): ");
                student[index_to_update].Birthday = Utils.GetDateTimeOnConsoleWithValidationAndFormat(Console.ReadLine(), "Birthday(dd.MM.yyyy): ", "\nPlease Enter Valid Birthday with these format (dd.MM.yyyy)\n\n");

                Console.Write("Gsm: ");
                student[index_to_update].Gsm = Utils.GetNumericValueWithValidation(Console.ReadLine(), "Gsm: ", "\nPlease Enter Valid Gsm with these format (5#########)\n\n", true, 10);


                string choice = "-1";
                while (choice != "9")
                {
                    Console.WriteLine("\nEnter Adress Number To Update || 9-EXIT");
                    choice = Console.ReadLine();
                    
                    if(choice == "1")
                    {
                        UpdateAddresses(student, id, 0);
                    }
                    else if(choice == "2" && student[index_to_update].NumberOfAdresses < 2)
                    {
                        Console.Write("\nThere is no adress" + choice + ". Please enter valid address number or press 0 to add new adress");
                        choice = Console.ReadLine();
                        if(choice == "0")
                        {
                            AddAlternativeAddresses( id);
                        }
                    }
                    else if(choice == "2" && student[index_to_update].NumberOfAdresses > 1)
                    {
                        UpdateAddresses(student, id, 1);
                    }
                    else if (choice == "3" && student[index_to_update].NumberOfAdresses < 3)
                    {
                        Console.Write("\nThere is no adress" + choice + ". Please enter valid address number or press 0 to add new adress");
                        choice = Console.ReadLine();
                        if (choice == "0")
                        {
                            AddAlternativeAddresses( id);
                        }
                    }
                    else if (choice == "3" && student[index_to_update].NumberOfAdresses > 2)
                    {
                        UpdateAddresses(student, id, 2);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease enter valid number...");
                    }
                   

                    Console.WriteLine("...Record Updated...");
                }
            }

        }

        public static void RemoveRecord(List<Student> student)
        {
            Console.Write("\n>>>Remove Record");

            Console.Write("\n\nStudentId to remove: ");
            string id = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);

            int indexToRemove = 0;//FindRecord(student, id);

            if (indexToRemove == -1)
            {
                Console.WriteLine("\n...Record cannot be found...\n");
                return;
            }
            else
            {
                PrintRecord("");

                Console.WriteLine("Are you sure Y/N: ");
                bool condition = true;

                while (condition)
                {
                    string yes_no = Console.ReadLine();

                    if (yes_no == "Y" || yes_no == "y")
                    {
                        student.RemoveAt(indexToRemove);
                        Console.WriteLine("\n...Record Removed...\n");
                        condition = false;
                    }
                    else if (yes_no == "N" || yes_no == "n")
                    {
                        Console.WriteLine("\n...Record Remove Canceled...\n");
                        condition = false;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nPlease Enter Y or N: ");
                    }
                }
            }
        }

        public static void SearchRecord()
        {
            Console.Write("\n>>>Search Record");

            Console.Write("\n\nStudentId to search: ");
            string id = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);

            PrintRecord(id);
        }

        public static int FindRecord(string Id)
        {
            var timeStart = DateTime.Now.TimeOfDay;
            try
            {
                string connetionString;
                SqlConnection cnn;
                SqlCommand cmd;

                connetionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                string query1 = "SELECT COUNT(StudentId) FROM [dbo].[Students] WHERE StudentId like '" + Id + "'";
                cmd = new SqlCommand(query1, cnn);
                SqlDataReader read = cmd.ExecuteReader();
                read.Read();
                string recordCount = read[0].ToString();

                if (recordCount == "1")
                {
                    Console.Write("Record is found in ");
                    Console.WriteLine(DateTime.Now.TimeOfDay - timeStart);
                    return -1;
                }
                else
                {
                    Console.Write("Record is not found in ");
                    Console.WriteLine(DateTime.Now.TimeOfDay - timeStart);
                    
                }

                cnn.Close();
                return 0;
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return 0;
            }
            
        }

        public static void PrintRecord(string id)
        {
            
            try
            {
                string connetionString;
                SqlConnection cnn;
                SqlCommand cmd, cmd2, cmd3;

                connetionString = @"Data Source=EM-SEMRA-K;Initial Catalog=master;Integrated Security=True";
                cnn = new SqlConnection(connetionString);
                cnn.Open();

                string query1 = "SELECT * FROM [dbo].[Students] WHERE StudentId like '" + id + "'";
                cmd = new SqlCommand(query1, cnn);
                SqlDataReader read = cmd.ExecuteReader();
                read.Read();

                int recordCount = FindRecord(id);
                if (recordCount == 1)
                {
                    Console.Write("Record is not found..");
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
                cnn.Close();
                cnn.Open();


                string query2 = "SELECT COUNT(StudentId) FROM [dbo].[Adresses] WHERE StudentId like '" + id + "'";
                cmd2 = new SqlCommand(query2, cnn);
                SqlDataReader read2 = cmd2.ExecuteReader();
                read2.Read();
                string recordCount2 = read2[0].ToString();
                for(int i= 1; i<=Convert.ToUInt32(recordCount2) ; i++)
                {
                    cnn.Close();
                    cnn.Open();
                    string query3 = "SELECT * FROM [dbo].[Adresses] WHERE StudentId like '" + id + "'" + "and AdressNo ="+ i.ToString() ;
                    Console.WriteLine("\n\tAdresses" + i.ToString());
                    cmd3 = new SqlCommand(query3, cnn);
                    SqlDataReader read3 = cmd3.ExecuteReader();
                    read3.Read();
                    Console.Write("\t\tStreet: ");
                    Console.WriteLine(read3[2].ToString());
                    Console.Write("\t\tNeighbourhood: ");
                    Console.WriteLine(read3[3].ToString());
                    Console.Write("\t\tDistrict: ");
                    Console.WriteLine(read3[4].ToString());
                    Console.Write("\t\tState: ");
                    Console.WriteLine(read3[5].ToString());
                    
                }

                cnn.Close();
                cnn.Dispose();
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

            }
            
        }

        public static string AddingId()
        {
            string toReturn = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
            bool isUniqueId = true;
            while (isUniqueId)
            {
                if (FindRecord(toReturn) == -1)
                {
                    Console.WriteLine("...This Student Id Has Already Recorded...\n\n");

                    PrintRecord(toReturn);
                    Console.WriteLine("\n...Do You Want To Enter Another Id (Y/N)...");

                    bool condition = true;
                    while (condition)
                    {
                        string yes_no = Console.ReadLine();

                        if (yes_no == "Y" || yes_no == "y")
                        {
                            Console.Write("Student Id: ");
                            toReturn = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
                            condition = false;
                        }
                        else if (yes_no == "N" || yes_no == "n")
                        {
                            toReturn = "false";
                            condition = false;
                            return toReturn;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter Y or N: ");
                        }
                    }

                }
                else
                {
                    isUniqueId = false;
                }

            }

            return toReturn;
        }
    }
}
