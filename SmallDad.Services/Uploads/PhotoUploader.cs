using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using SmallDad.Core.Config;
using SmallDad.Core.Dto;
using SmallDad.Core.Enumerations.Uploads;
using SmallDad.Core.Interfaces.Uploads;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmallDad.Services.Uploads
{
    // TODO: Use interface for DI
    public class PhotoUploader : IPhotoUploader
    {
        private readonly IHostingEnvironment _env;
        private string _photoThumbPath = string.Empty;
        private string _photoOriginalPath = string.Empty;
        private string _photoOriginalName = string.Empty;
        private string _photoThumbName = string.Empty;

        public PhotoUploader(IHostingEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task<PhotoUploadDto> Upload(IFormFile file, IFilePath filePath)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            string randomGuid = Guid.NewGuid().ToString();

            var imageExtension = Path.GetExtension(file.FileName);
            _photoOriginalName = randomGuid + imageExtension;
            _photoOriginalPath = Path.Combine(_env.ContentRootPath, filePath.Path, _photoOriginalName);

            using (var stream = new FileStream(_photoOriginalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Read from file
            using (MagickImage image = new MagickImage(_photoOriginalPath))
            {
                var photoThumbWidth = AppConstants.ProfilePhotoThumbSizeWidth.ToString();
                var photoThumbHeight = AppConstants.ProfilePhotoThumbSizeHeight.ToString();
                _photoThumbName = $"{randomGuid}-thumb-{photoThumbWidth}x{photoThumbHeight}{imageExtension}";
                _photoThumbPath = Path.Combine(_env.ContentRootPath, filePath.Path, _photoThumbName);

                MagickGeometry size = new MagickGeometry(100, 100);
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.
                size.IgnoreAspectRatio = true;

                image.Resize(size);
                image.AutoOrient();

                // Save the result
                image.Write(_photoThumbPath);
            }

            return new PhotoUploadDto
            {
                PhotoOriginalPath = Path.Combine(filePath.PublicPath, _photoOriginalName),
                PhotoThumbPath = Path.Combine(filePath.PublicPath, _photoThumbName)
            };
        }
    }
}
