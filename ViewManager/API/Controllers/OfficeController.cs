using API.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficeController : Controller
    {
        private readonly ViewManagerContext _db;

        public OfficeController(ViewManagerContext db) =>
            (_db) = (db);

        [Authorize(Roles = "Admin")]
        [HttpGet("GetOfficeList")]
        public async Task<IActionResult> GetOfficeList()
        {
            var officeList = await _db.Offices.ToListAsync();

            if(officeList.Count == 0)
            {
                return NotFound(nameof(officeList));
            }

            return Ok(officeList);
        }
    }
}
