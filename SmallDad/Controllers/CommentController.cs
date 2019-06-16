using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallDad.Infrastructure.Data;
using SmallDad.ViewModels.Comment;
using SmallDad.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmallDad.Core.Entities;

namespace SmallDad.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create(CreateCommentViewModel createCommentViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetCurrentUserAsync();

                var commentToDb = new Comment
                {
                    Author = currentUser,
                    AuthorId = currentUser.Id,
                    Content = createCommentViewModel.Content,
                    RankId = createCommentViewModel.Id
                };

                await _context.Comments.AddAsync(commentToDb);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("GetRank", "Rank", new { createCommentViewModel.Id });
        }

    }
}
