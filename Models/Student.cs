﻿using System;
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
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public int Registry { get; set; }

        // Listar
        public List<Student> ReadStudent(int? id = null)
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
        
        // Sobrescrever
        public bool RewriteFile(List<Student> studentsList)
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data\Base.json");

            var json = JsonConvert.SerializeObject(studentsList, Formatting.Indented);
            File.WriteAllText(filePath, json);

            return true;

        }

        // Inserir
        public void CreateStudent(Student student)
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

        public void UpdateStudent(Student student)
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