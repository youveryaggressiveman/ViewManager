using API.Core.Crypter;
using API.Entity;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ViewManagerContext _db;

        public AccountController(ViewManagerContext db)
        {
            _db = db;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token(string login, string password)
        {
            var identity = await GetIdentity(login, password);
            if (identity == null)
            {
                return NotFound(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                Token = encodedJwt,
                Id = identity.Name,
                RoleValue = identity.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value
            };           

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            User user = await _db.Users.FirstOrDefaultAsync(x => x.Login == login);

            if (user == null)
            {
                return null;
            }

            if (Encrypt.VerifyHashedPassword(user.Password, password))
            {
                var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                if (user != null && role != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Value)
                };
                    ClaimsIdentity claimsIdentity =
                        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                            ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }
            // если пользователя не найдено
            return null;
        }
    }
}
