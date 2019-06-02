using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallDad.Data;
using SmallDad.Misc;
using SmallDad.Models;

namespace SmallDad.Controllers
{
    public class RankController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult CreatePost()
        {
            return View();
        }
    }
}