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

                // return Ok(student.ReadStudent());
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

            return student.ReadStudent(id).FirstOrDefault();
        }

        [HttpPost]
        // POST: api/Student
        public IHttpActionResult Post(Student student)
        {
            try
            {
                Student _student = new Student();
                _student.CreateStudent(student);
                return Ok(_student.ReadStudent());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        [HttpPut]
        // PUT: api/Student/5
        public IHttpActionResult Put(int id, [FromBody]Student student)
        {
            try
            {
                Student _student = new Student();
                _student.Id = id;
                _student.UpdateStudent(student);
                return Ok(_student.ReadStudent(id).FirstOrDefault());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        [HttpDelete]
        // DELETE: api/Student/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Student _student = new Student();
                _student.DeleteStudent(id);
                return Ok("Student successfully deleted");
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);

            }
        }
    }
}
