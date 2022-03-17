using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookingDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int CabinID { get; set; }
    }
}
