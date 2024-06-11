using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class StudAnalyticController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudAnalyticController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudAnalytic>>> GetStudAnalytics()
        {
            return await _context.StudAnalytics
                                 .Include(sa => sa.Profwise)
                                 .Include(sa => sa.SubWise)
                                 .ToListAsync();
        }
        
        [HttpGet("/stu/{id}")]
        public async Task<ActionResult<StudAnalytic>> GetById(int id)
        {
            return await  _context.StudAnalytics
                .Include(sa => sa.Profwise)
                .Include(sa => sa.SubWise)
                .FirstOrDefaultAsync(sa=>sa.StuId==id);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudAnalytic>> GetStudAnalytic(int id)
        {
            var studAnalytic = await _context.StudAnalytics
                                             .Include(sa => sa.Profwise)
                                             .Include(sa => sa.SubWise)
                                             .FirstOrDefaultAsync(sa => sa.Id == id);

            if (studAnalytic == null)
            {
                return NotFound();
            }

            return studAnalytic;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudAnalytic(int id, StudAnalytic studAnalytic)
        {
            if (id != studAnalytic.Id)
            {
                return BadRequest();
            }

            var existingStudAnalytic = await _context.StudAnalytics
                                                     .Include(sa => sa.Profwise)
                                                     .Include(sa => sa.SubWise)
                                                     .FirstOrDefaultAsync(sa => sa.Id == id);

            if (existingStudAnalytic == null)
            {
                return NotFound();
            }

            existingStudAnalytic.RecentUpate = studAnalytic.RecentUpate;
            existingStudAnalytic.TotalLectures = studAnalytic.TotalLectures;

            _context.ProfAnalytics.UpdateRange(studAnalytic.Profwise);
            // existingStudAnalytic.Profwise = studAnalytic.Profwise;

            // Update SubWise
            _context.SubAnalytics.UpdateRange(studAnalytic.SubWise);
            // existingStudAnalytic.SubWise = studAnalytic.SubWise;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudAnalyticExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudAnalytic
        [HttpPost]
        public async Task<ActionResult<StudAnalytic>> PostStudAnalytic(StudAnalytic studAnalytic)
        {
            _context.StudAnalytics.Add(studAnalytic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudAnalytic", new { id = studAnalytic.Id }, studAnalytic);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudAnalytic>> DeleteStudAnalytic(int id)
        {
            var studAnalytic = await _context.StudAnalytics
                .Include(sa => sa.Profwise)
                .Include(sa => sa.SubWise)
                .FirstOrDefaultAsync(sa => sa.Id == id);
            if (studAnalytic == null)
            {
                return NotFound();
            }

            _context.StudAnalytics.Remove(studAnalytic);
            await _context.SaveChangesAsync();

            return studAnalytic;
        }

        private bool StudAnalyticExists(int id)
        {
            return _context.StudAnalytics.Any(e => e.Id == id);
        }
    }