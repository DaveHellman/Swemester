using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ProdMan_Server.Services
{
    public class UserInitilizer : IUserInitilizer
    {
        private readonly CabinDbContext db;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserInitilizer(
            CabinDbContext db,
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Init()
        {
            if (roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult()) return;

            roleManager.CreateAsync(new IdentityRole() { Name = "Admin" })
                .GetAwaiter()
                .GetResult();
            roleManager.CreateAsync(new IdentityRole() { Name = "Employee" })
                .GetAwaiter()
                .GetResult();
            roleManager.CreateAsync(new IdentityRole() { Name = "User" })
                .GetAwaiter()
                .GetResult();

            userManager.CreateAsync(new IdentityUser()
            {
                UserName = "Admin@home.se",
                Email = "Admin@home.se",
                EmailConfirmed = true
            }, "Pa$$w0rd")
                .GetAwaiter()
                .GetResult();

            userManager.CreateAsync(new IdentityUser() 
            { 
                UserName = "Employee@home.se",
                Email = "Employee@home.se",
                EmailConfirmed = true
            }, "Pa$$w0rd")
                .GetAwaiter()
                .GetResult();

            userManager.CreateAsync(new IdentityUser() 
            {
                UserName = "User@home.se",
                Email = "User@home.se",
                EmailConfirmed = true
            }, "Pa$$w0rd")
                .GetAwaiter()
                .GetResult();

            var user = db.Users.FirstOrDefault(u => u.Email == "Admin@home.se");
            userManager.AddToRoleAsync(user, "Admin")
                .GetAwaiter()
                .GetResult();

            var user2 = db.Users.FirstOrDefault(u => u.Email == "Employee@home.se");
            userManager.AddToRoleAsync(user2, "Employee")
                .GetAwaiter()
                .GetResult();

            var user3 = db.Users.FirstOrDefault(u => u.Email == "User@home.se");
            userManager.AddToRoleAsync(user3, "User")
                .GetAwaiter()
                .GetResult();
        }
    }
}
