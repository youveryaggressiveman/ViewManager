using API.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : Controller
    {
        private readonly ViewManagerContext _db;

        public SpecializationController(ViewManagerContext db) =>
            (_db) = (db);

        [HttpGet("GetSpecializationList")]
        public async Task<IActionResult> GetSpecializationList()
        {
            var specList = await _db.Specializations.ToListAsync();

            if(specList.Count == 0)
            {
                return NotFound(nameof(specList));
            }

            return Ok(specList);
        }
    }
}
