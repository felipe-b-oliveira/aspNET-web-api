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
    }
}