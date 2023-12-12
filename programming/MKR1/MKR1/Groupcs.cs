using System;

namespace MKR1
{
    public class Group
    {
        public int student_id { get; set; }
        public string group_name { get; set; }
        public int year { get; set; }

        public Group(int student_id, string group_name, int year)
        {
            this.student_id = student_id;
            this.group_name = group_name;
            this.year = year;
        }
    }
}