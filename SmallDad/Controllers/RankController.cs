using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmallDad.Data;
using SmallDad.Dto;
using SmallDad.Misc;
using SmallDad.Models;

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
        public IActionResult GetRank(int id, int? vote)
        {
            var rank = _context.Ranks.Where(x => x.Id == id).SingleOrDefault();

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
        public IActionResult CreatePost([Bind("Title,Description,CoverImage")] RankDto rankDto)
        {
            var imageExtension = Path.GetExtension(rankDto.CoverImage.FileName);
            var imageName = Guid.NewGuid().ToString() + imageExtension;

            var filePath = Path.Combine(_env.ContentRootPath, AppConstants.CoverImgPath, imageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                rankDto.CoverImage.CopyToAsync(stream);
            }

            var rank = new Rank
            {
                Title = rankDto.Title,
                Description = rankDto.Description,
                CoverImgPath = AppConstants.CoverImgPathPublic + imageName
            };

            _context.Ranks.Add(rank);
            _context.SaveChanges();

            return View();
        }
    }
}