using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCRUDOperations.Filters;
using WebApiCRUDOperations.Models;

namespace WebApiCRUDOperations.Controllers
{
    public class StudentController : ApiController
    {
        [Log]
        public IHttpActionResult GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (var context = new StudentEntities())
            {
                students = context.Students.ToList();
            }

            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found");
        }

        public IHttpActionResult GetStudentById(int id)
        {
            Student student = new Student();
            using (var context = new StudentEntities())
            {
                student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            }
            if (student == null)
                return Ok("No student with given ID found");
            else
                return Ok(student);
        }
        [Log]
        public IHttpActionResult GetStudentsByName(string firstName, string lastName)
        {
            List<Student> students = new List<Student>();
            using (var context = new StudentEntities())
            {
                students = context.Students.Where(s => s.FirstName == firstName & s.LastName == lastName).ToList();
            }
            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found with the given name");
        }

        public IHttpActionResult GetAllStudentsInSameStandard(string standard)
        {
            List<Student> students = new List<Student>();
            using (var context = new StudentEntities())
            {
                students = context.Students.Where(s => s.Standard == standard).ToList();
            }
            if (students.Count > 0)
                return Ok(students);
            else
                return Ok("No students found in the given standard");
        }

        public IHttpActionResult PostNewStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid student.");

            using (var context = new StudentEntities())
            {
                context.Students.Add(new Student()
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Standard = student.Standard
                });
                context.SaveChanges();
            }
            return Ok("New Student Added");
        }

        public IHttpActionResult Put(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid student");
            using (var context = new StudentEntities())
            {
                var existingStudent = context.Students.Where(s => s.StudentId == student.StudentId).FirstOrDefault();
                if (existingStudent == null)
                    return NotFound();
                else
                {
                    existingStudent.FirstName = student.FirstName;
                    existingStudent.LastName = student.LastName;
                    existingStudent.Standard = student.Standard;
                    context.SaveChanges();
                }
                return Ok();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            Student student = new Student();
            if (id <= 0)
                return BadRequest("Not a valid student id");
            using (var context = new StudentEntities())
            {
                student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
                if (student == null)
                    return NotFound();
                else
                {
                    context.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            return Ok();
        }
        [HttpGet]
        [Route("findStudent")]
        public HttpResponseMessage Get(int id)
        {
            Student student = new Student();
            using (var context = new StudentEntities())
            {
                student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            }
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, student);
        }

    }
}
