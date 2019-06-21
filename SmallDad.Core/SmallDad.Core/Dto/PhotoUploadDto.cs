using SmallDad.Core.Abstractions.Uploads;
using SmallDad.Core.Interfaces.Uploads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Core.Dto
{
    public class PhotoUploadDto
    {
        public string PhotoOriginalPath { get; set; }
        public string PhotoThumbPath { get; set; }
    }
}
