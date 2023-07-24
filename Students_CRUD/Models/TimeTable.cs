using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students_CRUD.Models
{
    public class TimeTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Depart { get; set; }

        public int? Sem { get; set; }

        public string? Division { get; set; }

        public DateTime RecentUpdatedDate { get; set; }

        public List<Day> Days { get; set; }

        // Static constructor to set up the default schedule
      

        public TimeTable()
        {
            RecentUpdatedDate = DateTime.Now;
           
        }

     

        
    }
}
