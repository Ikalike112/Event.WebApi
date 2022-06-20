using Event.Domain;
using Event.Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Event.Identity.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController
    {
        private readonly SignInManager<Speaker> _signInManager;
        private readonly UserManager<Speaker> _userManager;

        private readonly ApplicationSettings _appSettings;

        public AuthController(SignInManager<Speaker> signInManager,
    UserManager<Speaker> userManager, IOptions<ApplicationSettings> appSettings) =>
    (_signInManager, _userManager, _appSettings) =
    (signInManager, userManager, appSettings.Value);

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(LoginModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return new StatusCodeResult(404);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user,
                login.Password, false);
            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var token = GenerateJWTToken(user, roles);
                return token;
            }
            else
                return new StatusCodeResult(401);
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult<string>> Registration(RegisterModel register)
        {

            var user = new Speaker
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.Email,
                Email = register.Email
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Speaker");

                var roles = await _userManager.GetRolesAsync(user);
                var token = GenerateJWTToken(user,roles);
                return token;
            }

            if (result.Errors.First().Code == "DuplicateUserName")
                return new StatusCodeResult(409);
            return new StatusCodeResult(404);
            //return View(viewModel);
        }
        private string GenerateJWTToken(Speaker user, System.Collections.Generic.IList<string> roles)
        {

            try
            {

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
              //  claims.Add(new Claim("role", "user"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(securityToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }
    }
}
