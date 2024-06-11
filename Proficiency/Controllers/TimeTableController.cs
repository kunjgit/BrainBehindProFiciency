using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTablesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TimeTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/timetables
        [HttpGet]
        public ActionResult<IEnumerable<TimeTable>> Get()
        {
            return _context.TimeTables.Include(tt => tt.Days).ThenInclude(d => d.Lectures).ToList();
        }

        // GET api/timetables/{id}
        [HttpGet("{id}")]
        public ActionResult<TimeTable> Get(int id)
        {
            var timeTable = _context.TimeTables
                .Include(tt => tt.Days)
                .ThenInclude(d => d.Lectures)
                .FirstOrDefault(tt => tt.Id == id);

            if (timeTable == null)
            {
                return NotFound();
            }

            return timeTable;
        }

        // POST api/timetables
        [HttpPost]
        public ActionResult<TimeTable> Post([FromBody] TimeTable timeTable)
        {
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            timeTable.RecentUpdatedDate = TimeZoneInfo.ConvertTime(DateTime.Now,indiaTimeZone);

            _context.TimeTables.Add(timeTable);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = timeTable.Id }, timeTable);
        }

        [HttpPut("{id}")]
        public ActionResult<TimeTable> Put(int id, [FromBody] TimeTable updatedTimeTable)
        {
            var timeTable = _context.TimeTables
                .Include(tt => tt.Days)
                .ThenInclude(d => d.Lectures)
                .FirstOrDefault(tt => tt.Id == id);

            if (timeTable == null)
            {
                return NotFound();
            }

            timeTable.Depart = updatedTimeTable.Depart;
            timeTable.Sem = updatedTimeTable.Sem;
            timeTable.Division = updatedTimeTable.Division;
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            timeTable.RecentUpdatedDate = TimeZoneInfo.ConvertTime(DateTime.Now,indiaTimeZone);

            if (updatedTimeTable.Days != null)
            {
                foreach (var updatedDay in updatedTimeTable.Days)
                {
                    var existingDay = timeTable.Days.FirstOrDefault(d => d.Id == updatedDay.Id);
                    if (existingDay == null)
                    {
                        timeTable.Days.Add(updatedDay);
                    }
                    else
                    {
                        existingDay.DayName = updatedDay.DayName;
                        existingDay.Type = updatedDay.Type;
                        existingDay.Date = updatedDay.Date;

                        if (updatedDay.Lectures != null)
                        {
                            foreach (var updatedLecture in updatedDay.Lectures)
                            {
                                var existingLecture = existingDay.Lectures.FirstOrDefault(l => l.Id == updatedLecture.Id);
                                if (existingLecture == null)
                                {
                                    existingDay.Lectures.Add(updatedLecture);
                                }
                                else
                                {
                                    existingLecture.StartTime = updatedLecture.StartTime;
                                    existingLecture.EndTime = updatedLecture.EndTime;
                                    existingLecture.ProfName = updatedLecture.ProfName;
                                    existingLecture.Type = updatedLecture.Type;
                                    existingLecture.SubjectName = updatedLecture.SubjectName;
                                    existingLecture.Location = updatedLecture.Location;
                                }
                            }
                        }
                    }
                }
            }

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/timetables/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var timeTable = _context.TimeTables
                .Include(tt => tt.Days)
                .ThenInclude(d => d.Lectures)
                .FirstOrDefault(tt => tt.Id == id);
            
            
            // if timetable itself is right now delete we have to make sure that we are deleting the rootanalytics associated with it 

            var rana = _context.RootAnalytics.Where(ra => ra.Version == id).FirstOrDefault();
            if (timeTable == null)
            {
                return NotFound();
            }

            _context.RootAnalytics.Remove(rana);
            _context.TimeTables.Remove(timeTable);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
