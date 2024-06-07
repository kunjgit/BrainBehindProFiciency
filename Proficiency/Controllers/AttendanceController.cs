using Microsoft.AspNetCore.Mvc;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddAttendance([FromBody] Attendance attendance)
        {
            if (attendance == null)
                return BadRequest("Valid info missing");
            if (ModelState.IsValid)
            {
                _context.Attendances.Add(attendance);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public IActionResult GetAttendances()
        {
            var allAttendances = _context.Attendances.ToList();

            return Ok(allAttendances);
        }

        // GET api/attendance/id
        [HttpGet("{userId}")]
        public IActionResult GetFromID(int userId)
        {
            var allAttendances = _context.Attendances.Where(a => a.StudentId == userId).ToList();

            return Ok(allAttendances);
        }

        // GET api/attendance/{id}
        [HttpGet("{id}/subwise/{subjectname}")]
        public IActionResult GetFromSubject(int id, string subjectname)
        {
            var attendances = _context.Attendances.Where(a => a.StudentId == id && _context.Lectures.Any
            (lec => lec.Id == a.LectureId && lec.SubjectName == subjectname)).ToList();
            return Ok(attendances);

        }

        [HttpGet("{id}/profwise/{profname}")]
        public IActionResult GetFromProfName(int id, string profname)
        {
            var attendances = _context.Attendances.Where(a => a.StudentId == id && _context.Lectures.Any
            (lec => lec.Id == a.LectureId && lec.ProfName == profname)).ToList();
            return Ok(attendances);
        }


        [HttpGet("{id}/datewise/{date}")]
        public IActionResult GetFromDate(int id, string date)
        {
            var attendances = _context.Attendances.Where(a => a.StudentId == id && a.Date.Date.ToString() == date).ToList();
            return Ok(attendances);
        }


        // DELETE api/attendance/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var attendance = _context.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound();
            }
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{lectureid}/delete_date/{date}")]
        public IActionResult DeleteByLectureIdAndDate(string lectureid, string date)
        {
            var attendance = _context.Attendances.FirstOrDefault(a => a.LectureId == lectureid && a.Date.Date.ToString() == date);
            if (attendance == null)
            {
                return NotFound();
            }
            _context.Attendances.Remove(attendance);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
