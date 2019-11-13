using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace webApp.Models
{
    public class StudentModel
    {
        // Listar
        public List<StudentDTO> ReadStudent(int? id = null)
        {
            try
            {
                var studentBD = new StudentDAO();
                return studentBD.ReadStudentsDB(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while trying to list Students: Error => {ex.Message}");
            }
        }
        
        // Inserir
        public void CreateStudent(StudentDTO student)
        {
            try
            {
                var studentBD = new StudentDAO();
                studentBD.CreateStudentsDB(student);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while trying to insert Students: Error => {ex.Message}");
            }
        }

        public void UpdateStudent(StudentDTO student)
        {
            try
            {
                var studentBD = new StudentDAO();
                studentBD.UpdateStudentBD(student);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while trying to update Students: Error => {ex.Message}");
            }

        }

        public void DeleteStudent(int id)
        {
            try
            {
                var studentBD = new StudentDAO();
                studentBD.DeleteStudentBD(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while trying to delete Students: Error => {ex.Message}");
            }
        }

    }
}