using MKR1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Шаблони
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            List<Group> groups = new List<Group>();
            students.Add(new Student(1, "Hal", 2));
            students.Add(new Student(2, "Vohar", 2));
            students.Add(new Student(3, "Pivkach", 2));
            students.Add(new Student(4, "Adjeju", 3));

            groups.Add(new Group(1, "SA", 2023));
            groups.Add(new Group(2, "SA", 2022));
            groups.Add(new Group(3, "BA", 2020));
            groups.Add(new Group(4, "SO", 2018));

            taskB(groups, "SA");
            taskA(groups);
            taskC(students);

            static void taskA(List<Group> group_name)
            {
                var Unique = (from str in group_name select str.group_name).Distinct();
                foreach(var item in Unique)
                {
                    IEnumerable<Group> Users = (from user in group_name where user.group_name == item select user);
                    Console.WriteLine($"Група {item} Кількість студентів {Users.Count()}");
                }
            }

            static int taskB(List<Group> group_name, string name_group)
            {
                IEnumerable<Group> Users = (from user in group_name where user.group_name == name_group select user);
                Console.WriteLine($"Кількість студентів в групі {name_group} = {Users.Count()}");
                return Users.Count();
            }

            static void taskC(List<Student> students_list)
            {
                XDocument xmlDocument = new XDocument();
                XElement rootElement = new XElement("Students");
                foreach(var student in students_list)
                {
                    rootElement.Add(new XElement(student.surname,
                        new XElement("Id", student.id),
                        new XElement("Surname", student.surname),
                        new XElement("Course", student.course)));
                }
                xmlDocument.Add(rootElement);
                Console.WriteLine(xmlDocument.ToString());
                xmlDocument.Save(@$"{System.AppDomain.CurrentDomain.BaseDirectory}..\..\..\MKRData.xml");
            }
        }
    }
}
