using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proficiency.Models
{
    public class TimeTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Depart { get; set; }

        public int? Sem { get; set; }

        public string? Division { get; set; }

        public DateTime RecentUpdatedDate { get; set; }

        public List<Day> Days { get; set; }

        // Static constructor to set up the default schedule
        public TimeTable()
        {
            RecentUpdatedDate = DateTime.Now;
        }
    }
}
