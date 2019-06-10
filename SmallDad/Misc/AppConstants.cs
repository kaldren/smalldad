﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Misc
{
    public static class AppConstants
    {
        #region Ratings
        public const int RatingAwful = -20;
        public const int RatingSmells = -10;
        public const int RatingNormal = 0;
        public const int RatingCool = 10;
        public const int RatingBazooka = 20;
        #endregion

        #region Rank Cover Image
        public const string RankCoverImgPath = @"wwwroot\images\";
        public const string RankCoverImgPathPublic = @"\images\";
        #endregion Rank Cover Image

        #region Profile Photo
        public const string ProfilePhotoImgPath = @"wwwroot\images\profiles\";
        public const string ProfilePhotoImgPathPublic = @"\images\profiles\";
        #endregion Profile Photo

        #region Comments
        public const int CommentMinLength = 3;
        public const int CommentMaxLength = 500;
        #endregion Comments
    }
}
