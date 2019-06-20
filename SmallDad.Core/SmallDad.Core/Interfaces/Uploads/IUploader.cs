using Microsoft.AspNetCore.Http;
using SmallDad.Core.Enumerations.Uploads;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallDad.Core.Interfaces.Uploads
{
    interface IUploader
    {
        bool Upload(IFormFile file, FileUploadType FileUploadType);
    }
}
