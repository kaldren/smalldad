using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallDad.Data;

namespace SmallDad.Controllers
{
    public class RankController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RankController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            var listRanks = _context.Ranks.ToList();

            return View(listRanks);
        }

        [HttpGet("/rank/{id}")]
        public IActionResult GetRank(int id, int? vote)
        {
            var rank = _context.Ranks.Where(x => x.Id == id).SingleOrDefault();

            if (vote.HasValue)
            {
                var currentVote = vote == 1 ? 1 : -1;
                rank.Rating += currentVote;
                _context.SaveChanges();
            }

            return View(rank);
        }
    }
}