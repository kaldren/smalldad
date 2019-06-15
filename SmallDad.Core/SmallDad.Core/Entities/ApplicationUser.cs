using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallDad.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string ProfilePhotoThumbPath { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
