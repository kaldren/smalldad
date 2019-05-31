﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Models
{
    public enum RatingTypes
    {
        Awful,
        Smells,
        Normal,
        Cool,
        Bazooka
    }
    public class Rank
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public RatingTypes Verbal { get; set; }
    }
}