using SmallDad.Core.Config;
using SmallDad.Core.Entities;
using SmallDad.Core.Enumerations.Rank;
using System.Collections.Generic;

namespace SmallDad.Core.Entities
{
    public class Rank
    {
        private int _rating = 0;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumVotes { get; set; } = 0;
        public int Rating {
            get { return _rating; }
            set
            {
                _rating = value;
                UpdateVerbalRating();
            }
        }
        public RatingTypes Verbal { get; set; } = RatingTypes.Normal;
        public string CoverImgPath { get; set; }
        public List<Comment> Comments { get; set; }

        private void UpdateVerbalRating()
        {
            if (Rating < AppConstants.RatingAwful) Verbal = RatingTypes.Awful;
            else if (Rating < AppConstants.RatingSmells) Verbal = RatingTypes.Smells;
            else if (Rating > AppConstants.RatingNormal && Rating < AppConstants.RatingCool) Verbal = RatingTypes.Normal;
            else if (Rating > AppConstants.RatingCool && Rating < AppConstants.RatingBazooka) Verbal = RatingTypes.Cool;
            else if (Rating > AppConstants.RatingBazooka) Verbal = RatingTypes.Bazooka;
        }
    }
}
