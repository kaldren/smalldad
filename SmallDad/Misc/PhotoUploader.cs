using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Misc
{
    // TODO: Use interface for DI
    public class PhotoUploader
    {
        private readonly IHostingEnvironment _env;
        public string ImageName { get; set; }

        public PhotoUploader(IHostingEnvironment env)
        {
            _env = env;
        }
        public async Task<bool> Upload(IFormFile file, PhotoType photoType)
        {
            bool isSuccess = false;
            var imgPath = string.Empty;

            if (file == null)
            {
                return isSuccess;
            }

            switch (photoType)
            {
                case PhotoType.RankPhoto:
                    imgPath = AppConstants.RankCoverImgPath;
                    break;
                case PhotoType.ProfilePhoto:
                    imgPath = AppConstants.ProfilePhotoImgPath;
                    break;
                default:
                    break;
            }

            var imageExtension = Path.GetExtension(file.FileName);
            ImageName = Guid.NewGuid().ToString() + imageExtension;

            var filePath = Path.Combine(_env.ContentRootPath, imgPath, ImageName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                isSuccess = true;
            }

            return isSuccess;
        }
    }
}
