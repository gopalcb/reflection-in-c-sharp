using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Reflection
{
    public class Student
    {
        public string Name { get; set; }
        public string University { get; set; }
        public int Roll { get; set; }
    }

    public class StudentFunction
    {
        private Student student;
        public StudentFunction()
        {
            student = new Student
            {
                Name = "Gopal C. Bala",
                University = "Jahangirnagar University",
                Roll = 1424
            };
        }

        public string GetName()
        {
            return student.Name;
        }

        public string GetUniversity()
        {
            return student.University;
        }

        public int GetRoll()
        {
            return student.Roll;
        }
    }
}
