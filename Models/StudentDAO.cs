using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace webApp.Models
{
    public class StudentDAO
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DevConnection"].ConnectionString;
        //string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private IDbConnection connection;

        public StudentDAO()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        // Listar DB
        public List<Student> ReadStudentsDB()
        {
            var studentsList = new List<Student>();

            // Cria o comando
            IDbCommand selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Students";

            // Executa o comando
            IDataReader result = selectCmd.ExecuteReader();
            while (result.Read())
            {
                var std = new Student();

                std.Id = Convert.ToInt32(result["Id"]);
                std.Name = Convert.ToString(result["Name"]);
                std.LastName = Convert.ToString(result["LastName"]);
                std.Phone = Convert.ToString(result["Phone"]);
                std.Registry = Convert.ToInt32(result["Registry"]);

                studentsList.Add(std);
            }

            // Fecha a conexao
            connection.Close();

            return studentsList;
        }


        // Inserir DB
        public void CreateStudentsDB(Student student)
        {
            // Cria o comando
            IDbCommand createCmd = connection.CreateCommand();
            createCmd.CommandText = "INSERT INTO Students (Name, LastName, Phone, Registry) VALUES (@Name, @LastName, @Phone, @Registry)";

            IDbDataParameter paramName = new SqlParameter("Name", student.Name);
            createCmd.Parameters.Add(paramName);

            IDbDataParameter paramLastName = new SqlParameter("LastName", student.LastName);
            createCmd.Parameters.Add(paramLastName);

            IDbDataParameter paramPhone = new SqlParameter("Phone", student.Phone);
            createCmd.Parameters.Add(paramPhone);

            IDbDataParameter paramRegistry = new SqlParameter("Registry", student.Registry);
            createCmd.Parameters.Add(paramRegistry);

            createCmd.ExecuteNonQuery();
        }

        public Student UpdateStudentBD(int id, Student student)
        {
            var studentsList = new List<Student>();

            IDbCommand updateCmd = connection.CreateCommand();
            updateCmd.CommandText = "UPDATE Students SET Name = @Name, LastName = @LastName, Phone = @Phone, Registry = @Registry WHERE Id = @Id";

            IDbDataParameter paramId = new SqlParameter("id", student.Id);
            updateCmd.Parameters.Add(paramId);

            IDbDataParameter paramName = new SqlParameter("Name", student.Name);
            updateCmd.Parameters.Add(paramName);

            IDbDataParameter paramLastName = new SqlParameter("LastName", student.LastName);
            updateCmd.Parameters.Add(paramLastName);

            IDbDataParameter paramPhone = new SqlParameter("Phone", student.Phone);
            updateCmd.Parameters.Add(paramPhone);

            IDbDataParameter paramRegistry = new SqlParameter("Registry", student.Registry);
            updateCmd.Parameters.Add(paramRegistry);

            updateCmd.ExecuteNonQuery();

            return student;
        }


    }
}