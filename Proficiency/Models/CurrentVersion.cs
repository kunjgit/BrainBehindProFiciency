using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models;


public class CurrentVersion
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Version { get; set; }
    public int ActiveTTId { get; set; }
    
    public int ActiveRootAnalyticId { get; set; }
}