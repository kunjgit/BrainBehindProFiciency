using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Students_CRUD.Models;

namespace Students_CRUD.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options):base(options)
        {



         
            
        }



        public DbSet<Student> Students { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Lecture> Lectures { get; set; }


    }
}
