using System;
using System.Collections.Generic;

namespace studenrecordsystem
{
    public class Student
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public string StudentId { get; set; }

        public string Gsm { get; set; }

        public List<AdressClass> Adress { get; set; } 

        public int NumberOfAdresses { get; set; }
    }

}
