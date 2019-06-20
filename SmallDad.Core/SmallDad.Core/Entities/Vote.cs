using System;
using System.Collections.Generic;
using System.Text;

namespace SmallDad.Core.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
    }
}
