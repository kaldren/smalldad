using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.ViewModels.Comment
{
    public class CreateCommentViewModel
    {
        public string Content { get; set; }
        
        /// <summary>
        /// Id of the Rank
        /// </summary>
        public int Id { get; set; }
    }
}
