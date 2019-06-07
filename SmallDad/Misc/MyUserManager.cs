﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmallDad.Data;
using SmallDad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<string> UpdateBiography(string biography)
        {
            var userPrincipal = _httpContext.HttpContext.User;
            var user = await GetUserAsync(userPrincipal);
            user.Biography = biography;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user.Biography;
        }
    }
}
