using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webApp.Models;
using System.Web.Http.Cors;

namespace webApp.Controllers
{
    // Liberar o acesso para o teste de funcionamento da API
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        // GET: api/Student
        [HttpGet]
        [Route("Recover")]
        public IHttpActionResult Recover()
        {
            try
            {
                Student student = new Student();
                return Ok(student.ReadStudent());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // GET: api/Student/5
        [HttpGet]
        [Route("Recover/{Id}")]
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
        public Student Put(int id, [FromBody]Student student)
        {
            Student _student = new Student();

            return _student.UpdateStudent(id, student);
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {
            Student _student = new Student();

            _student.DeleteStudent(id);
        }
    }
}
