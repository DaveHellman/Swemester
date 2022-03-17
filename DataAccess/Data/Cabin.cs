using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Cabin
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public double Rent { get; set; }
        public int Area { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedDate { get; set; } = System.DateTime.Now;
        public DateTime UpdatedDate { get; set;} = System.DateTime.Now;

        public virtual List<Image> Images { get; set; } = new List<Image>();
        public virtual List<Booking> Bookings { get; set; } = new();
    }
}
