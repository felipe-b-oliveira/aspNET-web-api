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
        public List<StudentDTO> ReadStudentsDB(int? id = null)
        {
            var studentsList = new List<StudentDTO>();

            try
            {
                // Cria o comando
                IDbCommand selectCmd = connection.CreateCommand();

                if (id == null)
                {
                    selectCmd.CommandText = "SELECT * FROM Students";
                }
                else
                    selectCmd.CommandText = $"SELECT * FROM Students WHERE Id = {id}";

                // Executa o comando
                IDataReader result = selectCmd.ExecuteReader();
                while (result.Read())
                {
                    var std = new StudentDTO
                    {
                        Id = Convert.ToInt32(result["Id"]),
                        Name = Convert.ToString(result["Name"]),
                        LastName = Convert.ToString(result["LastName"]),
                        Phone = Convert.ToString(result["Phone"]),
                        Registry = Convert.ToInt32(result["Registry"]),
                    };

                    studentsList.Add(std);
                }

                return studentsList;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                // Fecha a conexao
                connection.Close();
            }

        }


        // Inserir DB
        public void CreateStudentsDB(StudentDTO student)
        {
            try
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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                // Fecha a conexao
                connection.Close();
            }
        }

        public StudentDTO UpdateStudentBD(StudentDTO student)
        {
            try
            {
                IDbCommand updateCmd = connection.CreateCommand();
                updateCmd.CommandText = "UPDATE Students SET Name = @Name, LastName = @LastName, Phone = @Phone, Registry = @Registry WHERE id = @Id";

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
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                // Fecha a conexao
                connection.Close();
            }

        }

        public void DeleteStudentBD(int id)
        {
            try
            {
                IDbCommand deleteCmd = connection.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM Students WHERE Id = @id";

                IDbDataParameter paramId = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                // Fecha a conexao
                connection.Close();
            }

        }
    }
}