using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers;
[Route("api/[controller]")]
    [ApiController]
    public class TimeTableAnalyticsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TimeTableAnalyticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        [HttpGet]
        public ActionResult<TimeTableAnalytic>  GetTimeTableAnalytic()
        {

            var timeTableAnalytic = _context.TimeTableAnalytics.FirstOrDefault();

            if (timeTableAnalytic == null)
            {
                return NotFound();
            }

            return timeTableAnalytic;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeTableAnalytic(TimeTableAnalytic timeTableAnalytic)
        {
            var existingAnalytic = await _context.TimeTableAnalytics.FindAsync(1);

            if (existingAnalytic != null)
            {
                return Conflict("TimeTableAnalytic already exists. Use PUT to update.");
            }

            var cvTask = _context.CurrentVersions.FirstOrDefault();
            var currentVersion = cvTask;
            if (currentVersion!=null)
            {
                int current_active_tt = currentVersion.ActiveTTId;
                timeTableAnalytic.version = current_active_tt;
            }

            _context.TimeTableAnalytics.Add(timeTableAnalytic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTimeTableAnalytic), new { id = timeTableAnalytic.Id }, timeTableAnalytic);
        }

        [HttpPut]
        public IActionResult UpdateTimeTableAnalytic(TimeTableAnalytic timeTableAnalytic)
        {
            _context.Entry(timeTableAnalytic).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteTimeTableAnalytic()
        {
            var timeTableAnalytic = _context.TimeTableAnalytics.Find(1);

            if (timeTableAnalytic == null)
            {
                return NotFound();
            }

            _context.TimeTableAnalytics.Remove(timeTableAnalytic);
            _context.SaveChanges();

            return NoContent();
        }
    }