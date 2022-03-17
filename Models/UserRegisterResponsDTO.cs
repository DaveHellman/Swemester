using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserRegisterResponsDTO
    {
        public bool IsRegistrationSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
