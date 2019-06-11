using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallDad.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string UserName { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string ProfilePhotoThumbPath { get; set; }
    }
}
