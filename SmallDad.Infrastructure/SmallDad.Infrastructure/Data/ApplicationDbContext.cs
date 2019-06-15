using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmallDad.Core.Entities;

namespace SmallDad.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Comment
            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Author)
                .WithMany(b => b.Comments)
                .HasForeignKey(p => p.AuthorId);
            #endregion Comment
        }

        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
