using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApp.Models;

namespace webApp.Controllers
{
    public class StudentController : ApiController
    {
        // GET: api/Student
        public IEnumerable<Student> Get()
        {
            Student student = new Student();

            return student.ReadStudent();
        }

        // GET: api/Student/5
        public Student Get(int id)
        {
            Student student = new Student();

            return student.ReadStudent().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Student
        public List<Student> Post(Student student)
        {
            Student _student = new Student();
            _student.CreateStudent(student);

            return _student.ReadStudent();
        }

        // PUT: api/Student/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {
        }
    }
}
