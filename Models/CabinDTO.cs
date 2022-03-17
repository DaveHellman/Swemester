using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CabinDTO
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 4000)]
        public double Rent { get; set; }
        public bool Available { get; set; }
        public string Description { get; set; }

        [Required]
        public string Adress { get; set; }
        public int Area { get; set; }
        public virtual List<ImageDTO> Images { get; set; } = new();
    }
}
