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
        public IActionResult Index()
        {
            var listRanks = _context.Ranks.ToList();

            return View(listRanks);
        }
    }
}