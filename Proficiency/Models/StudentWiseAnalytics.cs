using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models;

public class StudentWiseAnalytics
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id;
    public int student_id;
    public List<ProfWiseAnalytics> profwise;
    public List<SubWiseAnalytics> subwise;
}