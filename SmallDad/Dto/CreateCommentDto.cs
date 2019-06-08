using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Dto
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public int RankId { get; set; }
    }
}
