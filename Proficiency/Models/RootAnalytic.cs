using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models
{
    public class RootAnalytic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime LatestUpdate { get; set; }
        public int Version { get; set; }
        public int TotalLectures { get; set; }
        public List<ProfAnalytic> Profs { get; set; }
        public List<SubAnalytic> Subs { get; set; }

        public RootAnalytic()
        {
            TimeZoneInfo indiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            LatestUpdate = TimeZoneInfo.ConvertTime(DateTime.Now,indiaTimeZone);
            Version = 0;
            TotalLectures = 0;

        }
        
    }

}