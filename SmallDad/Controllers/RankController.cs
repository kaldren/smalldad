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
using System.Security.Claims;
using SmallDad.Services.Uploads;
using SmallDad.Core.Enumerations.Uploads;

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
                // Check if user already voted for this rank
                var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var hasUserVoted = await _context.Votes.SingleOrDefaultAsync(x => x.AuthorId == loggedInUserId);

                if (hasUserVoted != null)
                {
                    // User already voted for this rank
                    ModelState.AddModelError("AlreadyVoted", "You already voted for this");
                }
                else
                {
                    var currentVote = vote < 0 ? -1 : 1;
                    rank.Rating += currentVote;

                    // Increment number of votes
                    rank.NumVotes++;

                    // User voted for this rank
                    var voteDb = new Vote
                    {
                        AuthorId = loggedInUserId,
                        Value = currentVote
                    };
                    await _context.Votes.AddAsync(voteDb);

                    await _context.SaveChangesAsync();
                }
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
            var uploadedPhoto = await photoUploader.Upload(rankViewModel.CoverImage, FileUploadType.RankPhoto);

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