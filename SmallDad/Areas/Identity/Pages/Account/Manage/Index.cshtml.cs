﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmallDad.Misc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using SmallDad.Core.Entities;
using SmallDad.Services.Uploads;
using SmallDad.Core.Enumerations.Uploads;
using static SmallDad.Services.Uploads.PhotoUploader;

namespace SmallDad.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly MyUserManager _myUserManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _env;

        public IndexModel(
            MyUserManager myUserManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IHostingEnvironment env)
        {
            _myUserManager = myUserManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _env = env;
        }

        public string Username { get; set; }
        public string ProfilePhotoThumbPath { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [Bind("Email,PhoneNumber,Biography,ProfilePhoto,ProfilePhotoThumbPath")]
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            public string Biography { get; set; }
            public IFormFile ProfilePhoto { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()   
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var biography = await _userManager.Users
                            .Where(x => x.UserName == userName)
                            .Select(x => x.Biography)
                            .SingleOrDefaultAsync();
            var profilePhoto = await _userManager.Users
                            .Where(x => x.UserName == userName)
                            .Select(x => x.ProfilePhotoThumbPath)
                            .SingleOrDefaultAsync();

            Username = userName;
            ProfilePhotoThumbPath = profilePhoto;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                Biography = biography
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.Email != user.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            if (Input.PhoneNumber != user.PhoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (Input.Biography != user.Biography)
            {
                await _myUserManager.UpdateBiographyAsync(Input.Biography);
            }

            if (Input.ProfilePhoto != null)
            {
                if (Input.ProfilePhoto.Length > 0 && Input.ProfilePhoto.ContentType == "image/jpeg")
                {
                    var photoUploader = new PhotoUploader(_env);
                    var uploadedPhoto = await photoUploader.Upload(Input.ProfilePhoto, new ProfilePhotoUploadPath());
                    await _myUserManager.UpdatePhotoAsync(uploadedPhoto);
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
