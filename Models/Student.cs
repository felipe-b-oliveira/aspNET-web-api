using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace webApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public int Registry { get; set; }

        // Listar
        public List<Student> ReadStudent()
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = File.ReadAllText(filePath);
            var studentsList = JsonConvert.DeserializeObject<List<Student>>(json);

            return studentsList;
        }


        // Listar DB
        public List<Student> ReadStudentsDB()
        {

            // Variaveis de conexão ao banco
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projetos\CSharp\dotnetlearning\webApp\App_Data\Database.mdf;Integrated Security=True";
            IDbConnection connection;
            connection = new SqlConnection(connectionString);
            
            // Abre a conexão
            connection.Open();

            var studentsList = new List<Student>();

            // Cria o comando
            IDbCommand selectCmd = connection.CreateCommand();
            selectCmd.CommandText = "SELECT * FROM Students";

            // Executa o comando
            IDataReader result = selectCmd.ExecuteReader();
            while(result.Read())
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

        // Sobrescrever
        public bool RewriteFile(List<Student> studentsList)
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data\Base.json");

            var json = JsonConvert.SerializeObject(studentsList, Formatting.Indented);
            File.WriteAllText(filePath, json);

            return true;

        }

        // Inserir
        public Student CreateStudent(Student Student)
        {
            var studentsList = this.ReadStudent();

            var maxId = studentsList.Max(s => s.Id);
            Student.Id = maxId + 1;
            studentsList.Add(Student);

            RewriteFile(studentsList);
            return Student;
        }

        // Atualizar
        public Student UpdateStudent(int id, Student Student)
        {
            var studentsList = this.ReadStudent();

            var itemIndex = studentsList.FindIndex(s => s.Id == id);
            if(itemIndex >= 0)
            {
                Student.Id = id;
                studentsList[itemIndex] = Student;
            }
            else
            {
                return null;
            }

            RewriteFile(studentsList);
            return Student;
        }

        // Deletar
        public bool DeleteStudent(int id)
        {
            var studentsList = this.ReadStudent();

            var itemIndex = studentsList.FindIndex(s => s.Id == id);
            if (itemIndex >= 0)
            {
                studentsList.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            RewriteFile(studentsList);
            return true;
        }

    }
}