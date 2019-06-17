using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallDad.Infrastructure.Data;
using SmallDad.Misc;
using SmallDad.ViewModels.Rank;
using SmallDad.Core.Config;
using SmallDad.Core.Entities;
using SmallDad.Core.Enumerations.Rank;
using Microsoft.Extensions.Logging;

namespace SmallDad.Controllers
{
    [Authorize]
    public class RankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;
        private readonly ILogger<RankController> _logger;

        public RankController(ApplicationDbContext context, IHostingEnvironment env, ILogger<RankController> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var listRanks = _context.Ranks.ToList();

            return View(listRanks);
        }

        [HttpGet("/Rank/{id:int}")]
        public async Task<IActionResult> GetRank(int id, int? vote)
        {
            _logger.LogInformation($"Getting rank {id} with vote value of {vote}.");

            var rank = await _context.Ranks
                .Where(x => x.Id == id)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .SingleOrDefaultAsync();

            if (rank == null)
            {
                _logger.LogCritical($"Rank with id {id} does not exist.");
            }

            if (vote.HasValue)
            {
                var currentVote = vote < 0 ? -1 : 1;
                rank.Rating += currentVote;

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
            if (ModelState.IsValid == false)
            {
                return RedirectToAction(actionName: nameof(Create));
            }

            var photoUploader = new PhotoUploader(_env);
            var uploadedPhoto = await photoUploader.Upload(rankViewModel.CoverImage, PhotoType.RankPhoto);

            var rank = new Rank
            {
                Title = rankViewModel.Title,
                Description = rankViewModel.Description,
                CoverImgPath = uploadedPhoto != null ? AppConstants.RankCoverImgPathPublic + uploadedPhoto.PhotoOriginalPath : string.Empty
            };

            await _context.Ranks.AddAsync(rank);
            await _context.SaveChangesAsync();

            return View();
        }
    }
}