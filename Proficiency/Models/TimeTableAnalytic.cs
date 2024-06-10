using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Proficiency.Models;

public class TimeTableAnalytic
{
    public int Id { get; set; }
    public int version { get; set; }
    [NotMapped]
    public Dictionary<DayName, Dictionary<string, int>> subs { get; set; }
    [NotMapped]
    public Dictionary<DayName, Dictionary<string, int>> prof { get; set; }
    [NotMapped]
    public Dictionary<DayName,int>total { get; set; }
}