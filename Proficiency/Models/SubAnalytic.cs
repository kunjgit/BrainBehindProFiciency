using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models
{

    public class SubAnalytic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Sub { get; set; }
        public int Lectures { get; set; }

        public SubAnalytic()
        {
            this.Lectures = 0;
        }

    }

}