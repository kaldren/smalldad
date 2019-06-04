﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Dto
{
    public class RankDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile CoverImage { get; set; }
    }
}
