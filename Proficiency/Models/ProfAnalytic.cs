using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proficiency.Models
{


     public class ProfAnalytic
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          public string Professor { get; set; }
          public int Lectures { get; set; }


          public ProfAnalytic()
          {
               this.Lectures = 0;
          }
     }
}