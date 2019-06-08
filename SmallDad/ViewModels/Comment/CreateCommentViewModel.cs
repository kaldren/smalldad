using System;
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

        [Required(ErrorMessage = "Please write a comment.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Your comment should be between 3 and 60 characters.")]
        public string Content { get; set; }
        
        /// <summary>
        /// Id of the Rank
        /// </summary>
        public int Id { get; set; }
    }
}
