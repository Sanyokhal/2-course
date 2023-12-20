using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using modul_2.Models;
namespace modul_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\admin\Desktop\2-couse\programming\modul_2\modul_2\Database1.mdf"";Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                while (true)
                {
                    Console.WriteLine("a - список всіх автомобілів");
                    Console.WriteLine("b - Кількість машин марки X і які мають 7 в номері");
                    Console.WriteLine("c - Сумарна вартість машин марки X");
                    Console.WriteLine("d - Вихід");
                    Console.WriteLine("");
                    string select = Console.ReadLine();
                    select = select.ToLower();
                    if (select == "a")
                    {
                        var Auto_list = new List<Auto>();
                        string query = "SELECT * FROM Auto";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Auto auto = new Auto()
                                    {
                                        id = Convert.ToInt32(reader["id"]),
                                        surname = Convert.ToString(reader["surname"]),
                                        price = Convert.ToInt32(reader["price"]),
                                        mark = Convert.ToString(reader["mark"]),
                                        license = Convert.ToString(reader["license"]),
                                        address = Convert.ToString(reader["address"]),
                                    };
                                    Auto_list.Add(auto);
                                }
                            }
                        }
                        foreach (Auto Auto in Auto_list)
                        {
                            Console.WriteLine($"ID {Auto.id}  Surname : {Auto.surname} Price : {Auto.price} Mark : {Auto.mark} License: {Auto.license} Address: {Auto.address}");
                        }
                    }
                    else if (select == "b")
                    {
                        Console.WriteLine("Марка автомобіля : ");
                        string mark = Console.ReadLine();
                        int qty = 0;
                        var Auto_list = new List<Auto>();
                        string query = $"SELECT COUNT(id) as qty FROM Auto WHERE mark = '{mark}' AND license LIKE '%7%'";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    qty = Convert.ToInt32(reader["qty"]);
                                }
                            }
                        }
                            Console.WriteLine($"Кількість автомобілів з маркою {mark} та 7 в номері = {qty}");
                    }
                    else if (select == "c")
                    {
                        Console.WriteLine("Марка автомобіля : ");
                        string mark = Console.ReadLine();
                        int total = 0;
                        var Auto_list = new List<Auto>();
                        string query = $"SELECT SUM(price) as total FROM Auto WHERE mark = '{mark}' AND license LIKE '%7%'";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    total = Convert.ToInt32(reader["total"]);
                                }
                            }
                        }
                        Console.WriteLine($"Загальна сума автомобілів з маркою {mark} = {total}");
                    }
                    else if (select == "d")
                    {
                        Console.WriteLine("Закриття програми");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Хибний ввід");
                    }

                }
            }
        }
    }
    internal class Auto()
    {
        public int id;
        public string surname;
        public int price;
        public string mark;
        public string license;
        public string address;
        public string qty;
    }
}