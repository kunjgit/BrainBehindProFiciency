using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models;

public class SubWiseAnalytics
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id{ get; set; }
    public string sub_name{ get; set; }
    public int lectures { get; set; }

}