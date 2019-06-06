using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallDad.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Profile/{id:guid}")]
        public async Task<IActionResult> GetProfileById(Guid id)
        {
            var profile = await _context.Users
                            .Where(x => x.Id == id.ToString())
                            .SingleOrDefaultAsync();

            return View(profile);
        }
    }
}
