using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallDad.Data;
using SmallDad.ViewModels.Comment;
using SmallDad.Misc;
using SmallDad.Models;
using SmallDad.ViewModels.Rank;

namespace SmallDad.Controllers
{
    [Authorize]
    public class RankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public RankController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var listRanks = _context.Ranks.ToList();

            return View(listRanks);
        }

        [HttpGet("/Rank/{id:int}")]
        public async Task<IActionResult> GetRank(int id, int? vote)
        {
            var rank = await _context.Ranks
                .Where(x => x.Id == id)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .SingleOrDefaultAsync();

            if (vote.HasValue)
            {
                var currentVote = vote < 0 ? -1 : 1;
                rank.Rating += currentVote;

                if (rank.Rating < AppConstants.RatingAwful) rank.Verbal = RatingTypes.Awful;
                else if (rank.Rating < AppConstants.RatingSmells) rank.Verbal = RatingTypes.Smells;
                else if (rank.Rating > AppConstants.RatingNormal && rank.Rating < AppConstants.RatingCool) rank.Verbal = RatingTypes.Normal;
                else if (rank.Rating > AppConstants.RatingCool && rank.Rating < AppConstants.RatingBazooka) rank.Verbal = RatingTypes.Cool;
                else if (rank.Rating > AppConstants.RatingBazooka) rank.Verbal = RatingTypes.Bazooka;

                // Increment number of votes
                rank.NumVotes++;

                _context.SaveChanges();
            }

            return View(rank);
        }

        [HttpGet("/Rank/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/Rank/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("Title,Description,CoverImage")] RankViewModel rankViewModel)
        {
            var photoUploader = new PhotoUploader(_env);
            var isFileUploaded = await photoUploader.Upload(rankViewModel.CoverImage, PhotoType.RankPhoto);

            var rank = new Rank
            {
                Title = rankViewModel.Title,
                Description = rankViewModel.Description,
                CoverImgPath = isFileUploaded ? AppConstants.RankCoverImgPathPublic + photoUploader.ImageName : string.Empty
            };

            await _context.Ranks.AddAsync(rank);
            await _context.SaveChangesAsync();

            return View();
        }
    }
}