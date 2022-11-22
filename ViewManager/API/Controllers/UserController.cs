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
        private readonly ViewManagerContext _db;

        public UserController(ViewManagerContext db) =>
            (_db) = (db);

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
    }
}
