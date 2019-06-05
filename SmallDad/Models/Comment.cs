using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Content { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public int Rankid { get; set; }
        public Rank Rank { get; set; }
    }
}
