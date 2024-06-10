using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class RootAnalyticController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RootAnalyticController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RootAnalytic>>> GetRootAnalytics()
        {
            return await _context.RootAnalytics
                                 .Include(ra => ra.Profs)
                                 .Include(ra => ra.Subs)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RootAnalytic>> GetRootAnalytic(int id)
        {
            var rootAnalytic = await _context.RootAnalytics
                                             .Include(ra => ra.Profs)
                                             .Include(ra => ra.Subs)
                                             .FirstOrDefaultAsync(ra => ra.id == id);

            if (rootAnalytic == null)
            {
                return NotFound();
            }

            return rootAnalytic;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRootAnalytic(int id, RootAnalytic rootAnalytic)
        {
            if (id != rootAnalytic.id)
            {
                return BadRequest();
            }

            var existingRootAnalytic = await _context.RootAnalytics
                .Include(ra => ra.Profs)
                .Include(ra => ra.Subs)
                .FirstOrDefaultAsync(ra => ra.id == id);

            if (existingRootAnalytic == null)
            {
                return NotFound();
            }


            existingRootAnalytic.LatestUpdate = rootAnalytic.LatestUpdate;
            existingRootAnalytic.Version = rootAnalytic.Version;
            existingRootAnalytic.TotalLectures = rootAnalytic.TotalLectures;

       
            _context.ProfAnalytics.UpdateRange(rootAnalytic.Profs);
            // existingRootAnalytic.Profs = rootAnalytic.Profs;

     
            _context.SubAnalytics.UpdateRange(rootAnalytic.Subs);
            // existingRootAnalytic.Subs = rootAnalytic.Subs;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RootAnalyticExists(id))
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

        [HttpPost]
        public async Task<ActionResult<RootAnalytic>> PostRootAnalytic(RootAnalytic rootAnalytic)
        {
            _context.RootAnalytics.Add(rootAnalytic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRootAnalytic", new { id = rootAnalytic.id }, rootAnalytic);
        }

    
        [HttpDelete("{id}")]
        public async Task<ActionResult<RootAnalytic>> DeleteRootAnalytic(int id)
        {
            var rootAnalytic = await _context.RootAnalytics
                .Include(ra => ra.Profs)
                .Include(ra => ra.Subs)
                .FirstOrDefaultAsync(ra => ra.id == id);
            if (rootAnalytic == null)
            {
                return NotFound();
            }

            _context.RootAnalytics.Remove(rootAnalytic);
            await _context.SaveChangesAsync();

            return rootAnalytic;
        }

        private bool RootAnalyticExists(int id)
        {
            return _context.RootAnalytics.Any(e => e.id == id);
        }
    }