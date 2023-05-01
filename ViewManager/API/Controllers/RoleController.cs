using API.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly ViewManagerContext _db;

        public RoleController(ViewManagerContext db) =>
            (_db) = (db);

        [HttpGet("GetRoleList")]
        public async Task<IActionResult> GetRoleList()
        {
            var userList = await _db.Roles.ToListAsync();

            if (userList.Count == 0)
            {
                return NotFound(nameof(userList));
            }

            return Ok(userList);
        }
    }
}
