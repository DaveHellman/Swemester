using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class CabinDbContext : IdentityDbContext<IdentityUser>
    {
        public CabinDbContext(DbContextOptions<CabinDbContext> opt) : base(opt)
        {

        }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
