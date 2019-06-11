using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallDad.Data;
using SmallDad.ViewModels.Profile;
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

            var profileViewModel = new ProfileViewModel
            {
                Biography = profile.Biography,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                ProfilePhotoPath = profile.ProfilePhotoPath,
                ProfilePhotoThumbPath = profile.ProfilePhotoThumbPath
            };

            return View(profileViewModel);
        }

        [HttpGet("/Profile/{username:alpha}")]
        public async Task<IActionResult> GetProfileByUserName(string username)
        {
            var profile = await _context.Users
                            .Where(x => x.UserName == username)
                            .SingleOrDefaultAsync();

            var profileViewModel = new ProfileViewModel
            {
                Biography = profile.Biography,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                ProfilePhotoPath = profile.ProfilePhotoPath,
                ProfilePhotoThumbPath = profile.ProfilePhotoThumbPath
            };

            return View(profileViewModel);
        }
    }
}
