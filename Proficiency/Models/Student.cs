using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proficiency.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Depart { get; set; }

        public int? Sem { get; set; }

        public string? Division { get; set; }

        public string? Batch { get; set; }
    }
}
