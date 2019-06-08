﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.ViewModels.Comment
{
    public class CreateCommentViewModel
    {
        /// <summary>
        /// Content of the comment
        /// </summary>

        [Required]
        public string Content { get; set; }
        
        /// <summary>
        /// Id of the Rank
        /// </summary>
        public int Id { get; set; }
    }
}
