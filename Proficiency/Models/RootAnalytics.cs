using System.ComponentModel.DataAnnotations;

namespace Proficiency.Models;
public class RootAnalytics
{
    [Key]
     public int id { get; set; }

     DateTime latest_update{ get; set; }
     public int version{ get; set; }
     int total_lectures{ get; set; }
     List<ProfWiseAnalytics> profs{ get; set; }
     List<SubWiseAnalytics> subs{ get; set; }
}