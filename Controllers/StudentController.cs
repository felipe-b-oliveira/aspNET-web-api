using App.Domain;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using webApp.Models;

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
                StudentModel student = new StudentModel();

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
        public IHttpActionResult RecoverById(int id)
        {
            try
            {
                StudentModel student = new StudentModel();
                return Ok(student.ReadStudent(id).FirstOrDefault());
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

        }

        [HttpPost]
        // POST: api/Student
        public IHttpActionResult Post(StudentDTO student)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                StudentModel _student = new StudentModel();
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
        public IHttpActionResult Put(int id, [FromBody]StudentDTO student)
        {
            try
            {
                StudentModel _student = new StudentModel();
                student.Id = id;
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
                StudentModel _student = new StudentModel();
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
