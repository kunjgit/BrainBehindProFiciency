using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models;

public class ProfWiseAnalytics
{    
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     int id{ get; set; }
     string prof_name{ get; set; }
     int lectures{ get; set; }
}
