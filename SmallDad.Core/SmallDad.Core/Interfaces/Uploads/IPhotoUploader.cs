using Microsoft.AspNetCore.Http;
using SmallDad.Core.Dto;
using SmallDad.Core.Enumerations.Uploads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallDad.Core.Interfaces.Uploads
{
    public interface IPhotoUploader
    {
        Task<PhotoUploadDto> Upload(IFormFile file, FileUploadType FileUploadType);
    }
}
