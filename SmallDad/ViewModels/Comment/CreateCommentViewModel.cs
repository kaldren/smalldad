using SmallDad.Core.Config;
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
        [StringLength(
            AppConstants.CommentMaxLength, 
            MinimumLength = AppConstants.CommentMinLength, 
            ErrorMessage = "Your comment should be between {2} and {1} characters.")]
        public string Content { get; set; }
        
        /// <summary>
        /// Id of the Rank
        /// </summary>
        public int Id { get; set; }
    }
}
