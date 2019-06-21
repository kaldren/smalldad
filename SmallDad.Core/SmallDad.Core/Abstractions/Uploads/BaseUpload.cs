using Microsoft.AspNetCore.Http;
using SmallDad.Core.Enumerations.Uploads;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallDad.Core.Abstractions.Uploads
{
    public abstract class BaseUpload
    {
        public abstract Task<object> Upload(IFormFile file, FileUploadType FileUploadType);
    }
}
