using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6_SQL.Models
{
    internal class EmployeesRepository : IRepository<Employee>
    {
        protected SqlConnection _connection;
        public EmployeesRepository(SqlConnection connection)
        {
            _connection = connection;
        }
        public List<Employee> GetAll()
        {
            var Employees = new List<Employee>();
            string query = "SELECT * FROM Personal LEFT JOIN Computers ON Personal.pc_model = Computers.model";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee Employee = new Employee()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            surname = Convert.ToString(reader["surname"]),
                            room_num = Convert.ToInt32(reader["room_num"]),
                            department_name = Convert.ToString(reader["department_name"]),
                            pc_model = Convert.ToString(reader["pc_model"]),
                            cpu = Convert.ToString(reader["cpu"]),
                            graphics_card = Convert.ToString(reader["graphics_card"]),
                            mother_board = Convert.ToString(reader["mother_board"]),
                            RAM = Convert.ToInt32(reader["RAM"])
                        };
                        Employees.Add(Employee);
                    }
                }
            }
            return Employees;
        }
        public List<Employee> RoomNumMore(int room_num)
        {
            var Employees = new List<Employee>();
            string query = $"SELECT * FROM Personal LEFT JOIN Computers ON Personal.pc_model = Computers.model WHERE room_num > '{room_num}'";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee Employee = new Employee()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            surname = Convert.ToString(reader["surname"]),
                            room_num = Convert.ToInt32(reader["room_num"]),
                            department_name = Convert.ToString(reader["department_name"]),
                            pc_model = Convert.ToString(reader["pc_model"]),
                            cpu = Convert.ToString(reader["cpu"]),
                            graphics_card = Convert.ToString(reader["graphics_card"]),
                            mother_board = Convert.ToString(reader["mother_board"]),
                            RAM = Convert.ToInt32(reader["RAM"])
                        };
                        Employees.Add(Employee);
                    }
                }
            }
            return Employees;
        }
        public List<Employee> GetByRam(int ram)
        {
            var Employees = new List<Employee>();
            string query = $"SELECT * FROM Personal LEFT JOIN Computers ON Personal.pc_model = Computers.model WHERE RAM = '{ram}'";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee Employee = new Employee()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            surname = Convert.ToString(reader["surname"]),
                            room_num = Convert.ToInt32(reader["room_num"]),
                            department_name = Convert.ToString(reader["department_name"]),
                            pc_model = Convert.ToString(reader["pc_model"]),
                            cpu = Convert.ToString(reader["cpu"]),
                            graphics_card = Convert.ToString(reader["graphics_card"]),
                            mother_board = Convert.ToString(reader["mother_board"]),
                            RAM = Convert.ToInt32(reader["RAM"])
                        };
                        Employees.Add(Employee);
                    }
                }
            }
            return Employees;
        }
        public List<Employee> GetDepartments()
        {
            var Employees = new List<Employee>();
            string query = "SELECT DISTINCT department_name FROM Personal";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee Employee = new Employee()
                        {
                            department_name = Convert.ToString(reader["department_name"]),
                        };
                        Employees.Add(Employee);
                    }
                }
            }
            return Employees;
        }
        public List<Employee> GetOddEmployees()
        {
            var Employees = new List<Employee>();
            string query = "SELECT * FROM Personal LEFT JOIN Computers ON Personal.pc_model = Computers.model WHERE room_num % 2 = 0";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee Employee = new Employee()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            surname = Convert.ToString(reader["surname"]),
                            room_num = Convert.ToInt32(reader["room_num"]),
                            department_name = Convert.ToString(reader["department_name"]),
                            pc_model = Convert.ToString(reader["pc_model"]),
                            cpu = Convert.ToString(reader["cpu"]),
                            graphics_card = Convert.ToString(reader["graphics_card"]),
                            mother_board = Convert.ToString(reader["mother_board"]),
                            RAM = Convert.ToInt32(reader["RAM"])
                        };
                        Employees.Add(Employee);
                    }
                }
            }
            return Employees;
        }
        public List<Employee> GetModelsList()
        {
            var Employees = new List<Employee>();
            string query = "SELECT COUNT(id) as 'qty', pc_model FROM Personal GROUP BY pc_model ORDER BY COUNT(id) DESC";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee Employee = new Employee()
                        {
                            id = Convert.ToInt32(reader["qty"]),
                            pc_model = Convert.ToString(reader["pc_model"]),
                        };
                        Employees.Add(Employee);
                    }
                }
            }
            return Employees;
        }
        public List<Employee> OrderByRam()
        {
            var Employees = new List<Employee>();
            string query = "SELECT * FROM Personal LEFT JOIN Computers ON Personal.pc_model = Computers.model ORDER BY RAM DESC, room_num DESC";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee Employee = new Employee()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            surname = Convert.ToString(reader["surname"]),
                            room_num = Convert.ToInt32(reader["room_num"]),
                            department_name = Convert.ToString(reader["department_name"]),
                            pc_model = Convert.ToString(reader["pc_model"]),
                            cpu = Convert.ToString(reader["cpu"]),
                            graphics_card = Convert.ToString(reader["graphics_card"]),
                            mother_board = Convert.ToString(reader["mother_board"]),
                            RAM = Convert.ToInt32(reader["RAM"])
                        };
                        Employees.Add(Employee);
                    }
                }
            }
            return Employees;
        }
        public bool EditEmployee(int id, string surname, int room_num, string department, string pc_model)
        {
            var Employees = new List<Employee>();
            string query = $"UPDATE Personal SET surname='{surname}', room_num='{room_num}', department_name='{department}', pc_model='{pc_model}' WHERE id = '{id}'";
            using (SqlCommand command = new SqlCommand(query, _connection)) {
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}