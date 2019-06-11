﻿using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SmallDad.Dto;
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
        private string _photoThumbPath = string.Empty;
        private string _photoOriginalPath = string.Empty;
        private string _photoOriginalName = string.Empty;
        private string _photoThumbName = string.Empty;
        private string _imgPath = string.Empty;

        public PhotoUploader(IHostingEnvironment env)
        {
            _env = env;
        }
        public async Task<PhotoUploadDto> Upload(IFormFile file, PhotoType photoType)
        {
            string randomGuid = Guid.NewGuid().ToString();

            if (file == null)
            {
                return null;
            }

            switch (photoType)
            {
                case PhotoType.RankPhoto:
                    _imgPath = AppConstants.RankCoverImgPath;
                    break;
                case PhotoType.ProfilePhoto:
                    _imgPath = AppConstants.ProfilePhotoImgPath;
                    break;
                default:
                    break;
            }

            var imageExtension = Path.GetExtension(file.FileName);
            _photoOriginalName = randomGuid + imageExtension;
            _photoOriginalPath = Path.Combine(_env.ContentRootPath, _imgPath, _photoOriginalName);

            using (var stream = new FileStream(_photoOriginalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Read from file
            using (MagickImage image = new MagickImage(_photoOriginalPath))
            {
                _photoThumbName = randomGuid + "-thumb-100x100" + imageExtension;
                _photoThumbPath = Path.Combine(_env.ContentRootPath, _imgPath, _photoThumbName);

                MagickGeometry size = new MagickGeometry(100, 100);
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.
                size.IgnoreAspectRatio = true;

                image.Resize(size);

                // Save the result
                image.Write(_photoThumbPath);
            }

            return new PhotoUploadDto
            {
                PhotoOriginalPath = Path.Combine(_imgPath, _photoOriginalName),
                PhotoThumbPath = Path.Combine(_imgPath, _photoThumbName)
            };
        }
    }
}
