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

        [Authorize(Roles = "Teacher")]
        [HttpGet("GetLogByOffice")]
        public async Task<IActionResult> GetLogByOffice(string officeName)
        {
            var logs = await _db.LogByOffices.Where(log => log.OfficeId == officeName).ToListAsync();

            if (logs == null)
            {
                return NotFound(nameof(logs));
            }

            return Ok(logs);
        }
    }
}
