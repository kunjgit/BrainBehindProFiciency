using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proficiency.Data;
using Proficiency.Models;

namespace Proficiency.Controllers;
[Route("api/[controller]")]
    [ApiController]
    public class CurrentVersionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CurrentVersionController(ApplicationDbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        public async Task<ActionResult<CurrentVersion>> GetCurrentVersion()
        {
            var currentVersion = await _context.CurrentVersions.FirstOrDefaultAsync();

            if (currentVersion == null)
            {
                return NotFound();
            }

            return currentVersion;
        }

 
        [HttpPut]
        public async Task<IActionResult> PutCurrentVersion(CurrentVersion currentVersion)
        {
            var existingVersion = await _context.CurrentVersions.FirstOrDefaultAsync();

            if (existingVersion == null)
            {
                return NotFound();
            }

            existingVersion.Version = currentVersion.Version;
            existingVersion.ActiveTTId = currentVersion.ActiveTTId;
            existingVersion.ActiveRootAnalyticId = currentVersion.ActiveRootAnalyticId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<CurrentVersion>> PostCurrentVersion(CurrentVersion currentVersion)
        {
            var existingVersion = await _context.CurrentVersions.FirstOrDefaultAsync();

            if (existingVersion != null)
            {
                return Conflict("A CurrentVersion object already exists. Use PUT to update it.");
            }

            _context.CurrentVersions.Add(currentVersion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrentVersion", new { id = currentVersion.Id }, currentVersion);
        }

        private bool CurrentVersionExists(int id)
        {
            return _context.CurrentVersions.Any(e => e.Id == id);
        }
    }