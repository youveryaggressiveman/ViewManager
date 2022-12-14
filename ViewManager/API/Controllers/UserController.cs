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
                Include(r=>r.Role)
                .FirstOrDefaultAsync(us => us.Id == id);

            if(user == null)
            {
                return NotFound(nameof(user));
            }

            return Ok(user);
        }

        [Authorize(Roles = "Accountant")]
        [HttpGet("GetUserList")] 
        public async Task<IActionResult> GetUserList()
        {
            var userList = await _db.Users.Where(us => us.RoleId == 1).ToListAsync();

            if (userList.Count == 0)
            {
                return NotFound(nameof(userList));
            }

            return Ok(userList);
        }

        [Authorize(Roles = "Accountant")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody]User user)
        {
            if (user == null)
            {
                return NotFound(nameof(user));
            }

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

        [Authorize(Roles = "Accountant")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]User newUser)
        {
            if (newUser == null)
            {
                return NotFound(nameof(newUser));
            }

            var credential = _credentialCreator.RandomString(6);

            newUser.Id = Guid.NewGuid().ToString();
            newUser.Login = credential[0];
            newUser.Password = Encrypt.HashPassword(credential[1]);

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
    }
}
