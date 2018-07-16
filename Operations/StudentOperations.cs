using System;

namespace studenrecordsystem
{
    class StudentOperations
    {
        public static Student GetStudentInformations(bool isAdd, string studentIdToUpdate)
        {
            Student toReturnStudent = new Student();
            
            Console.Write("Name: ");
            toReturnStudent.Name = Console.ReadLine();

            Console.Write("Surname: ");
            toReturnStudent.Surname = Console.ReadLine();

            Console.Write("Birthday(dd.MM.yyyy): ");
            toReturnStudent.Birthday = Utils.GetDateTimeOnConsoleWithValidationAndFormat(Console.ReadLine(), "\nBirthday(dd.MM.yyyy): ", "Please Enter Valid Birthday with these format (dd.MM.yyyy)\n\n");

            if (isAdd)
            {
                Console.Write("StudentId: ");
                toReturnStudent.StudentId = AddOperations.AddingId();
            }
            else
            {
                toReturnStudent.StudentId = studentIdToUpdate;
            }

            Console.Write("Gsm: ");
            toReturnStudent.Gsm = Utils.GetNumericValueWithValidation(Console.ReadLine(), "Gsm: ", "\nPlease Enter Valid Gsm with these format (5#########)\n\n", true, 10);
            return toReturnStudent;
        }
    }
}
