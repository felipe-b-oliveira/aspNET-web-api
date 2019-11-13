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
        public IEnumerable<Students> Get()
        {
            Students student = new Students();

            return student.studentsList();
        }

        // GET: api/Student/5
        public Students Get(int id)
        {
            Students student = new Students();

            return student.studentsList().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Student
        public List<Students> Post(Students student)
        {
            List<Students> studentList = new List<Students>();
            studentList.Add(student);

            return studentList;
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
