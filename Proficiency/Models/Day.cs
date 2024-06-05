using System.ComponentModel.DataAnnotations;

namespace Proficiency.Models
{
    public enum DayType
    {
        RegularDay,
        Holiday,
        Exam
    }

    public enum DayName
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public class Day
    {
        [Key]
        public int Id { get; set; }

        public DayName DayName { get; set; }

        public DayType Type { get; set; }

        public DateTime Date { get; set; }

        public List<Lecture> Lectures { get; set; }
    }
}
