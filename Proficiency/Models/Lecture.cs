using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proficiency.Models
{

    public enum LectureType
    {
        Lab,
        RegularClass
    }

    public class Lecture
    {
        [Key]
        public string Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ProfName { get; set; }

        public LectureType Type { get; set; }

        public string SubjectName { get; set; }

        public string Location { get; set; }

        public Lecture()
        {
            GenerateId();
        }

        private void GenerateId()
        {

            string startTimeString = StartTime.ToString("yyyyMMddHHmmss");
            string endTimeString = EndTime.ToString("yyyyMMddHHmmss");
            string randomString = Guid.NewGuid().ToString().Substring(0, 4);

            Id = $"{startTimeString}_{endTimeString}_{randomString}";
        }
    }
}
