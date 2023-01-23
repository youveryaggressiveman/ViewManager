using API.Core.Crypter;
using API.Core.Data;
using API.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AutoCredentialCreator _credentialCreator;
        private readonly ViewManagerContext _db;

        public UserController(ViewManagerContext db)
        {
            _db = db;
            _credentialCreator = new AutoCredentialCreator();
        }

        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _db.Users.
                Include(r => r.Role)
                .FirstOrDefaultAsync(us => us.Id == id);

            if (user == null)
            {
                return NotFound(nameof(user));
            }

            user.Password = string.Empty;

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var userList = await _db.Users.Where(us => us.RoleId == 1).ToListAsync();

            if (userList.Count == 0)
            {
                return NotFound(nameof(userList));
            }

            foreach (var user in userList)
            {
                user.Password = string.Empty;
            }

            return Ok(userList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null)
            {
                return NotFound(nameof(user));
            }

            if (!await CheckLogin(user.Login, user.Id))
            {
                return BadRequest("This login is already taken:" + user.Login);
            }

            user.Office = null;
            user.Role = null;

            user.Password = Encrypt.HashPassword(user.Password);

            try
            {
                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return NotFound(nameof(newUser));
            }

            var credential = _credentialCreator.RandomString(6);

            newUser.Id = Guid.NewGuid().ToString();
            newUser.Login = credential[0];
            newUser.Password = Encrypt.HashPassword(credential[1]);

            newUser.Office = null;
            newUser.Role = null;

            foreach (var item in newUser.Specializations)
            {
                _db.Attach(item);
            }

            if (!await CheckLogin(newUser.Login))
            {
                return BadRequest("The password that was generated was taken. Try again:" + newUser.Login);
            }

            try
            {
                _db.Users.Add(newUser);
                await _db.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        private async Task<bool> CheckLogin(string value, string? Id = null)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(user => user.Login == value);

            if (existingUser == null)
            {
                return true;
            }

            if (existingUser?.Id == Id)
            {
                return true;
            }

            return false;
        }
    }
}
