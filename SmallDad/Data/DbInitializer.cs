using SmallDad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Ranks.Any())
            {
                return;
            }

            var ranks = new Rank[]
            {
                new Rank {Title = "Huawei P30 Pro", Description="Huawei P30 Pro is a phone made by Huawei", Rating = 0, NumVotes = 0, Verbal = RatingTypes.Normal },
                new Rank {Title = "Boyko Borisov", Description="Prime minister of Bulgaria", Rating = 0, NumVotes = 0, Verbal = RatingTypes.Normal },
                new Rank {Title = "Vacation in Cuba", Description="Cuba is a country", Rating = 0, NumVotes = 0, Verbal = RatingTypes.Normal }
            };

            foreach (var rank in ranks)
            {
                context.Ranks.Add(rank);
            }

            context.SaveChanges();
        }
    }
}
