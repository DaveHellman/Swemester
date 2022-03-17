using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using ProdMan_WEBAPI.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProdMan_WEBAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly APISettings apiSettings;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<APISettings> settings
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            apiSettings = settings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestDTO userreq)
        {
            UserRegisterResponsDTO response = new();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = userreq.Username,
                    Email = userreq.Email,
                    Name = userreq.Name };

                var regResult = await userManager.CreateAsync(user, userreq.Password);

                if (regResult.Succeeded)
                {
                    try
                    {
                        var storedUser = await userManager.FindByEmailAsync(userreq.Email);
                        var roleReg = await userManager.AddToRoleAsync(storedUser, "User");
                        response.IsRegistrationSuccess = true;
                        return StatusCode(201, response);
                    }
                    catch(Exception ex)
                    {
                        response.IsRegistrationSuccess = false;
                        response.ErrorMessages.Add(ex.Message);
                        return BadRequest(response);
                    }
                }
                response.ErrorMessages = regResult.Errors.Select(e => e.Description).ToList();
                response.IsRegistrationSuccess = false;
                return BadRequest(response);
            }
            else
            {
                response.ErrorMessages.Add("Formuläret är ej korrekt ifyllt");
                response.IsRegistrationSuccess = false;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(AuthenticationRequestDTO authReq)
        {
            if (authReq == null || !ModelState.IsValid) return BadRequest("Invalid");

            var signInResult = await signInManager.PasswordSignInAsync(
                authReq.Username,
                authReq.Password,
                false,
                false);

            if (!signInResult.Succeeded)
            {
                var userresponse = new AuthenticationResponsDTO()
                {
                    IsAuthenticationSuccess = false,
                    ErrorMessage = "Wrong Username or Password!"
                };
                return Unauthorized(userresponse);
            }

            var AppUser = await userManager.FindByNameAsync(authReq.Username);
            if (AppUser == null) return NotFound();

            var claims = await GetClaimsAsync(AppUser);
            var credential = GetSignInCredentials();

            var tokenDesciptor = new JwtSecurityToken(
                issuer: apiSettings.ValidIssuer,
                audience: apiSettings.ValidAudience,
                claims: claims,
                signingCredentials: credential,
                expires:System.DateTime.Now.AddDays(1)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDesciptor);

            var respons = new AuthenticationResponsDTO()
            {
                IsAuthenticationSuccess = true,
                Token = token,
                UserDTO = new UserDTO()
                {
                    Email = AppUser.Email,
                    UserName = AppUser.UserName,

                }
            };

            return Ok(respons);
        }

        private SigningCredentials GetSignInCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettings.SecretKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return credential;
        }

        private async Task<List<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Id", user.Id)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
