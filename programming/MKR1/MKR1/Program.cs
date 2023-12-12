using System;

namespace MKR1
{ 
    public class Student
    {
       public int id { get; set; }
        public string surname { get; set; }
        public int course { get; set; }

        public Student(int id, string surname, int course)
        {
            this.id = id;
            this.surname = surname;
            this.course = course;
        }
    }
}