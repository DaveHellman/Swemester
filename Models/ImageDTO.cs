using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ImageDTO
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool IsPrimary { get; set; }
        public int CabinID { get; set; }
    }
}
