using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace studenrecordsystem
{
    class Operations
    {
        public static void AddRecord(List<Student> student)
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
            string temp = AddingId(student);

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

            toadd.Adress.Add(AddAddress(student));
            toadd.NumberOfAdresses = 1;
            for (int i = 0; i < 2; i++)
            {
                AdressClass adresstoadd = new AdressClass();
                adresstoadd.Street = " ";
                adresstoadd.Neighborhood = " ";
                adresstoadd.District = " ";
                adresstoadd.State = " ";
                toadd.Adress.Add(adresstoadd);
            }

            student.Add(toadd);
            
            bool condition = true;

            while (condition)
            {
                Console.WriteLine("Dou you want to add more adress Y/N: ");
                string yes_no = Console.ReadLine();

                if (yes_no == "Y" || yes_no == "y")
                {
                    AddAlternativeAddresses(student, toadd.StudentId);
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


            Console.WriteLine("\n...Added...\n");
        }

        public static AdressClass AddAddress(List<Student> student)
        {
            AdressClass adresstoadd = new AdressClass();
            Console.WriteLine("Please Enter Adress Informations: ");

            Console.Write("Street: ");
            adresstoadd.Street = Console.ReadLine();

            Console.Write("Neighborhood: ");
            adresstoadd.Neighborhood = Console.ReadLine();

            Console.Write("District: ");
            adresstoadd.District = Console.ReadLine();

            Console.Write("State: ");
            adresstoadd.State = Console.ReadLine();

            return adresstoadd;

        }

        public static void AddAlternativeAddresses(List<Student> student, string Id)
        {
            int index = FindRecord(student, Id);
            int NumberOfAdress = student[index].NumberOfAdresses;
            if (NumberOfAdress == 3)
            {
                Console.WriteLine("You Have Already Add Three Adress. You Cannot Add More Adress Information");
            }
            else
            {
                Console.WriteLine("Please Enter Adress Informations: ");

                Console.Write("Street: ");
                student[index].Adress[NumberOfAdress].Street = Console.ReadLine();

                Console.Write("Neighborhood: ");
                student[index].Adress[NumberOfAdress].Neighborhood = Console.ReadLine();

                Console.Write("District: ");
                student[index].Adress[NumberOfAdress].District = Console.ReadLine();

                Console.Write("State: ");
                student[index].Adress[NumberOfAdress].State = Console.ReadLine();

                student[index].NumberOfAdresses++;
            }

        }

        public static void UpdateAddresses(List<Student> student, string Id, int indexAdress)
        {
            int index = FindRecord(student, Id);
            
                Console.WriteLine("Please Enter Adress Informations: ");

                Console.Write("Street: ");
                student[index].Adress[indexAdress].Street = Console.ReadLine();

                Console.Write("Neighborhood: ");
                student[index].Adress[indexAdress].Neighborhood = Console.ReadLine();

                Console.Write("District: ");
                student[index].Adress[indexAdress].District = Console.ReadLine();

                Console.Write("State: ");
                student[index].Adress[indexAdress].State = Console.ReadLine();
            
        }

        public static void UpdateRecord(List<Student> student)
        {
            Console.Write("\n>>>Update Record");

            Console.Write("\n\nEnter Student Id to Update: ");
            string id = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);

            int index_to_update = FindRecord(student, id);
            if (index_to_update == -1)
            {
                Console.WriteLine("\n...Record cannot be found...\n");
                return;
            }
            else
            {
                PrintRecord(student, index_to_update);

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
                    switch (choice)
                    {
                        case "1":
                            UpdateAddresses(student, id, 0);
                            break;

                        case "2":
                            UpdateAddresses(student, id, 1);
                            break;

                        case "3":
                            UpdateAddresses(student, id, 2);
                            break;

                        default:
                            Console.WriteLine("Please Enter Valid Adress Number\n");
                            break;
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

            int indexToRemove = FindRecord(student, id);

            if (indexToRemove == -1)
            {
                Console.WriteLine("\n...Record cannot be found...\n");
                return;
            }
            else
            {
                PrintRecord(student, indexToRemove);

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

        public static void SearchRecord(List<Student> student)
        {
            Console.Write("\n>>>Search Record");

            Console.Write("\n\nStudentId to search: ");
            string id = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);

            int indexToRemove = FindRecord(student, id);

            if (indexToRemove == -1)
            {
                Console.WriteLine("\n...Record cannot be found...\n");
                return;
            }
            else
            {
                PrintRecord(student, indexToRemove);
            }
        }

        public static int FindRecord(List<Student> student, string Id)
        {
            var timeStart = DateTime.Now.TimeOfDay;

            for (int counter = 0; counter < student.Count; counter++)
            {
                if (student[counter].StudentId == Id)
                {
                    Console.Write("Record is found in ");
                    Console.WriteLine(DateTime.Now.TimeOfDay - timeStart);
                    return counter;
                }
            }

            Console.Write("Record is not found in ");
            Console.WriteLine(DateTime.Now.TimeOfDay - timeStart);

            return -1;
        }

        public static bool WriteToTxt(List<Student> student)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\emre.bulkurcuoglu\Desktop\studenrecordsystem\studenrecordsystem\output.txt", false))
                {
                    for (int counter = 0; counter < student.Count; counter++)
                    {
                        file.WriteLine(student[counter].Name + " " + student[counter].Surname + " " + student[counter].Birthday.ToString("dd.MM.yyyy") + " " + student[counter].StudentId + " " + student[counter].Gsm);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

                return false;
            }

        }

        public static void PrintRecord(List<Student> student, int recordIndex)
        {
            Console.Write("Name: ");
            Console.WriteLine(student[recordIndex].Name);

            Console.Write("Surname: ");
            Console.WriteLine(student[recordIndex].Surname);

            Console.Write("Birthday: ");
            Console.WriteLine(student[recordIndex].Birthday.ToString().Substring(0, 10));

            Console.Write("Student Id: ");
            Console.WriteLine(student[recordIndex].StudentId);

            Console.Write("Gsm: ");
            Console.WriteLine(student[recordIndex].Gsm);

            Console.WriteLine("Adresses");

            for(int i = 1; i<=student[recordIndex].Adress.Count; i++)
            {
                Console.WriteLine("\n\tAdress" + i + ": ");
                Console.WriteLine("\t\tStreet: " + student[recordIndex].Adress[i - 1].Street);
                Console.WriteLine("\t\tNeighborhood: " + student[recordIndex].Adress[i - 1].Neighborhood);
                Console.WriteLine("\t\tDistrict: " + student[recordIndex].Adress[i - 1].District);
                Console.WriteLine("\t\tState: " + student[recordIndex].Adress[i - 1].State);
            }
        }

        public static string AddingId(List<Student> student)
        {
            string toReturn = Utils.GetNumericValueWithValidation(Console.ReadLine(), "StudentId: ", "\nPlease Enter Valid StudentId with these format (#########)\n\n", true, 9);
            bool isUniqueId = true;
            while (isUniqueId)
            {
                if (FindRecord(student, toReturn) != -1)
                {
                    Console.WriteLine("...This Student Id Has Already Recorded...\n\n");

                    PrintRecord(student, FindRecord(student, toReturn));
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

        public static bool ReadFromTxt(List<Student> student)
        {

            try
            {
                var file = new StreamReader(@"C:\Users\emre.bulkurcuoglu\Desktop\studenrecordsystem\studenrecordsystem\output.txt");
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        Student temp = new Student();

                        var delimiters = new char[] { ' ' };
                        var segments = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                        temp.Name = segments[0];
                        temp.Surname = segments[1];
                        temp.Birthday = Utils.GetDateTimeOnConsoleWithValidationAndFormat(segments[2], "", "");
                        temp.StudentId = segments[3];
                        temp.Gsm = segments[4];

                        student.Add(temp);
                    }

                    file.Close();
                    return true;
                }
            }

            catch (Exception e)
            {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public static bool ReadFromXml(List<Student> student)
        {
            try
            {
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\Users\emre.bulkurcuoglu\Desktop\studenrecordsystem\studenrecordsystem\record.xlsx")))
                {
                    var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;


                    for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                    {
                        var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                        Student temp = new Student();

                        temp.Name = row.ElementAt<string>(0);
                        temp.Surname = row.ElementAt<string>(1);
                        temp.Birthday = Utils.GetDateTimeOnConsoleWithValidationAndFormat(row.ElementAt<string>(2).Substring(0, 10), "", "");
                        temp.StudentId = row.ElementAt<string>(3);
                        temp.Gsm = row.ElementAt<string>(4);

                        student.Add(temp);
                    }
                    return true;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public static bool WriteToXml(List<Student> student)
        {

            try
            {
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\Users\emre.bulkurcuoglu\Desktop\studenrecordsystem\studenrecordsystem\record.xlsx")))
                {
                    var myWorksheet = xlPackage.Workbook.Worksheets.First();
                    int totalRows = student.Count;

                    for (int rowNum = 1; rowNum <= totalRows; rowNum++) //select starting row here
                    {
                        xlPackage.Workbook.Worksheets.First().Cells[rowNum, 1].Value = student[rowNum - 1].Name;
                        xlPackage.Workbook.Worksheets.First().Cells[rowNum, 2].Value = student[rowNum - 1].Surname;
                        xlPackage.Workbook.Worksheets.First().Cells[rowNum, 3].Value = Utils.GetDateTimeOnConsoleWithValidationAndFormat(student[rowNum - 1].Birthday.ToString("dd.MM.yyyy"), "", "").ToString().Substring(0, 10);
                        xlPackage.Workbook.Worksheets.First().Cells[rowNum, 4].Value = student[rowNum - 1].StudentId;
                        xlPackage.Workbook.Worksheets.First().Cells[rowNum, 5].Value = student[rowNum - 1].Gsm;
                    }
                    xlPackage.Save();
                    return true;
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
