using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models
{

    public class StudAnalytic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StuId { get; set; }
        public DateTime RecentUpate{ get; set; }
        public int TotalLectures{ get; set; }
        public List<ProfAnalytic> Profwise{ get; set; }
        public List<SubAnalytic> SubWise{ get; set; }

        public StudAnalytic()
        {
            this.TotalLectures = 0;
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            this.RecentUpate = TimeZoneInfo.ConvertTime(DateTime.Now,indiaTimeZone);

        }
    }
}