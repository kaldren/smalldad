using SmallDad.Core.Entities;
using SmallDad.Core.Enumerations.Rank;
using System.Collections.Generic;

namespace SmallDad.Core.Entities
{
    public class Rank
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumVotes { get; set; } = 0;
        public int Rating { get; set; } = 0;
        public RatingTypes Verbal { get; set; } = RatingTypes.Normal;
        public string CoverImgPath { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
