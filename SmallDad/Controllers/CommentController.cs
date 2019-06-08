using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallDad.Data;
using SmallDad.ViewModels.Comment;
using SmallDad.Misc;
using SmallDad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly MyUserManager _userManager;

        public CommentController(ApplicationDbContext context, MyUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("/Comment/{id:int}")]
        public async Task<IActionResult> Create(int id, string content)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetCurrentUser();

                var commentToDb = new Comment
                {
                    Author = currentUser,
                    AuthorId = currentUser.Id,
                    Content = content,
                    RankId = id
                };

                await _context.Comments.AddAsync(commentToDb);
                await _context.SaveChangesAsync();

                var CommentCreatedViewModel = new CommentCreatedViewModel
                {
                    RankId = id
                };

                return RedirectToAction("GetRank", "Rank", new { id });
            }

            return View();
        }

    }
}
