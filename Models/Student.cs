using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using Newtonsoft.Json;

namespace webApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Registry { get; set; }

        // Listar
        public List<Student> ReadStudent()
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data\Base.json");
            var json = File.ReadAllText(filePath);
            var studentsList = JsonConvert.DeserializeObject<List<Student>>(json);

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