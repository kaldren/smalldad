using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Biography { get; set; }
    }
}
