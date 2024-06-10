using Microsoft.EntityFrameworkCore;
using Proficiency.Models;
namespace Proficiency.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ProfAnalytic> ProfAnalytics { get; set; }
        public DbSet<SubAnalytic>SubAnalytics { get; set; }
        public DbSet<StudAnalytic>StudAnalytics { get; set; }
        public DbSet<RootAnalytic>RootAnalytics { get; set; }
        
        
        
        
        

    }
}
