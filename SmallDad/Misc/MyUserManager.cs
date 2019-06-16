using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmallDad.Core.Dto;
using SmallDad.Core.Entities;
using SmallDad.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallDad.Misc
{
    public class MyUserManager : UserManager<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public MyUserManager
            (IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger, ApplicationDbContext context, IHttpContextAccessor httpContext) : 
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<string> UpdateBiographyAsync(string biography)
        {
            var userPrincipal = _httpContext.HttpContext.User;
            var user = await GetUserAsync(userPrincipal);
            user.Biography = biography;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user.Biography;
        }

        public async Task<PhotoUploadDto> UpdatePhotoAsync(PhotoUploadDto photoDto)
        {
            var userPrincipal = _httpContext.HttpContext.User;
            var user = await GetUserAsync(userPrincipal);
            user.ProfilePhotoPath = photoDto.PhotoOriginalPath;
            user.ProfilePhotoThumbPath = photoDto.PhotoThumbPath;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return photoDto;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await GetUserAsync(_httpContext.HttpContext.User);
        }
    }
}
