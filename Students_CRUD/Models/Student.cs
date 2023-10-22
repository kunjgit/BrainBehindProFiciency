using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students_CRUD.Models
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
