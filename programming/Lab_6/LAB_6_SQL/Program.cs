using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LAB_6_SQL.Models;
using Microsoft.Data.SqlClient;
namespace LAB_6_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Завдання
            ////1.Використовуючи інструмент SQL Server Object Explorer у Visual Studio створити БД з іменем
            //назва_групи_прізвище_студента.
            ////2.Створити таблицю у базі даних у відповідності до індивідуального варіанту.Наповнити таблицю даними
            //(не менше 20 записів).
            //3.Cтворити наступні види SQL - запитів:
            //a) простий запит на вибірку;
            //b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
            //c) запит зі складним критерієм;
            //d) запит з унікальними значеннями;
            //e) запит з використанням обчислювального поля;
            //f) запит з групуванням по заданому полю, використовуючи умову групування;
            //g) запит із сортування по заданому полю в порядку зростання та спадання значень;
            //h) запит з використанням дій по модифікації записів.

            //Варіант 5.Створити таблицю бази даних про співробітників, що мають комп'ютер: прізвище, номер
            //кімнати, назва відділу, дані про комп'ютери.


            //Лабораторна 6
            //Використовуючи SqlCommand підготувати програмну оболонку для виконання завдань лабораторної роботи 5.
            //Забезпечити користувачу можливість ввести значення параметрів запиту.


            const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\admin\Desktop\programming_labs\LAB_6_SQL\LAB_6_SQL\Database1.mdf"";Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                var rep = new EmployeesRepository(connection);

                while (true)
                {
                Start: Console.WriteLine("Таблиця Працівників. Список можливих операцій:");
                    Console.WriteLine("1 - вибрати всіх працівників");
                    Console.WriteLine("2 - вибрати всіх працівників, у яких номер кімнати більше числа X");
                    Console.WriteLine("3 - вибрати всіх працівників, у яких кількість ОЗУ = вказаній кількості гб");
                    Console.WriteLine("4 - вивесті список всіх департаментів");
                    Console.WriteLine("5 - вивести працівникиків, номер кімнати яких є парним");
                    Console.WriteLine("6 - вивести кількість моделей комп'ютерів в порядку спадання");
                    Console.WriteLine("7 - вивести працівників у у порядку спадання кількості ОЗУ та номеру кімнати");
                    Console.WriteLine("8 - редагування працівника за id");
                    Console.WriteLine("0 - вихід");
                    Console.WriteLine("============================================");
                    string request = Console.ReadLine();
                    List<Employee> employees = rep.GetAll();
                    if (request == "1")
                    {
                        employees = rep.GetAll();
                    }
                    else if(request == "2")
                    {
                        Console.Write("Введіть число :");
                        int num = Convert.ToInt32(Console.ReadLine());
                        employees = rep.RoomNumMore(num);
                    }
                    else if (request == "3")
                    {
                        Console.Write("Введіть кількість ГБ :");
                        int ram = Convert.ToInt32(Console.ReadLine());
                        employees = rep.GetByRam(ram);
                    }
                    else if (request == "4")
                    {
                        employees = rep.GetDepartments();
                    }
                    else if (request == "5")
                    {
                        employees = rep.GetOddEmployees();
                    }
                    else if (request == "6")
                    {
                        employees = rep.GetModelsList();
                    }
                    else if (request == "7")
                    {
                        employees = rep.OrderByRam();
                    }
                    else if (request == "8")
                    {
                        Console.WriteLine("Редагування працівника");

                        Console.Write("id працівника : ");
                        int id = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Прізвище працівника : ");
                        string surname = Console.ReadLine();

                        Console.Write("Номер кімнати : ");
                        int room_num = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Назва департаменту : ");
                        string department = Console.ReadLine();

                        Console.Write("Модель Комп'ютера : ");
                        string pc_model = Console.ReadLine();

                        bool resp = rep.EditEmployee(id, surname, room_num, department, pc_model);
                        if (resp)
                        {
                            Console.WriteLine("Змінено успішно");
                        }
                        goto Start;
                    }
                    else if (request == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Даного варіанту немає");
                        goto End;
                    }


                    if (request == "4")
                    {
                        Console.WriteLine("Список Департаментів");
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"{employee.department_name}");
                        }
                    }else if(request == "6")
                    {
                        Console.WriteLine("Список моделей");
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"Кількість {employee.id} штук(-и) Модель {employee.pc_model}");
                        }
                    }
                    else
                    {
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"ID: {employee.id} SURNAME {employee.surname} DEPARTMENT {employee.department_name} ROOM {employee.room_num} PC_MODEL {employee.pc_model} CPU {employee.cpu} RAM {employee.RAM}ГБ");
                        }
                    }
                End: Console.ReadLine();
                    goto Start;
                }
            }

        }
    }
}