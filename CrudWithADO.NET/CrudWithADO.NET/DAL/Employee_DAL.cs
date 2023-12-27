using CrudWithADO.NET.Models;
using System.Data;
using System.Data.SqlClient;
     
namespace CrudWithADO.NET.DAL
{
    public class Employee_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            configuration = builder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetAll()
        {
            List<Employee> empList = new List<Employee>();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_Employee]";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = Convert.ToInt32(dr["Id"]);
                    emp.FirstName = dr["FirstName"].ToString();
                    emp.LastName = dr["LastName"].ToString();
                    emp.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                    emp.Email = dr["Email"].ToString();
                    emp.Salary = Convert.ToDouble(dr["Salary"]);
                    empList.Add(emp);
                }
                _connection.Close();
            }
            return empList;
        }

        public bool Insert(Employee model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Insert_Employee]";
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Get_EmployeeById]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    employee.Id = Convert.ToInt32(dr["Id"]);
                    employee.FirstName = dr["FirstName"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                    employee.Email = dr["Email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["Salary"]);
                }
                _connection.Close();
            }
            return employee;
        }

        public bool Update(Employee model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_update_Employee]";
                _command.Parameters.AddWithValue("@Id", model.Id);
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int deleteRowCount = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[usp_Delete_Employee]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                deleteRowCount = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return deleteRowCount > 0 ? true : false;
        }

    }
}
