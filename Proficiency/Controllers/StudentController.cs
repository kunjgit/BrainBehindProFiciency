using Microsoft.AspNetCore.Mvc;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return _context.Students.ToList();
        }

        // GET api/students/{id}
        [HttpGet("{id}")]
        public ActionResult<Student> Get(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // POST api/students
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student student)
        {

            _context.Students.Add(student);

            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        // PUT api/students/{id}
        [HttpPut("{id}")]
        public ActionResult<Student> Put(int id, [FromBody] Student updatedStudent)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = updatedStudent.Name;
            student.Depart = updatedStudent.Depart;
            student.Sem = updatedStudent.Sem;
            student.Division = updatedStudent.Division;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/students/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
