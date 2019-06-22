using SmallDad.Core.Config;
using SmallDad.Core.Interfaces.Uploads;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallDad.Services.Uploads
{
    public class RankPhotoUploadPath : IFilePath
    {
        public string Path { get; } = AppConstants.RankCoverImgPath;
        public string PublicPath { get; } = AppConstants.RankCoverImgPathPublic;
    }
}
