// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServicesPlomberie.Data;
using ServicesPlomberie.Models;
using SQLitePCL;
using Microsoft.AspNetCore.Mvc;

namespace ServicesPlomberie.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Utilisateur> _signInManager;
        private readonly UserManager<Utilisateur> _userManager;
        private readonly IUserStore<Utilisateur> _userStore;
        private readonly IUserEmailStore<Utilisateur> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;


        public RegisterModel(
            UserManager<Utilisateur> userManager,
            IUserStore<Utilisateur> userStore,
            SignInManager<Utilisateur> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment hosting,
            ApplicationDbContext context)
            
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;


        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel

        {
            public List<Specialite> sousServices { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 


            [Display(Name = "picture")]
            public string Pictue { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "firstName")]
            public string firstName { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "LastName")]
            public string lastName { get; set; }
            [Display(Name = "LastName")]
            
            public Boolean prestataire {  get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            
            [Required]
            [Display(Name = "phoneNumber")]
            public string PhoneNumber { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string? role { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList {  get; set; }
            [ValidateNever]
            public List<Service> services { get; set; }
            [Display(Name = "region")]
            public string region { get; set; }

          
            [Display(Name = "service")]
            public string service { get; set; }
            [Display(Name = "genre")]
            public string genre { get; set; }

            [Display(Name = "sousservice")]
            public string sousservice { get; set; }

            

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Input = new InputModel()
            {

                RoleList = _roleManager.Roles.Select(x => x.Name)
                                      .Where(roleName => roleName != "admin")
                                      .Select(roleName => new SelectListItem
                                      {
                                          Text = roleName,
                                          Value = roleName
                                      })
                                      .ToList(),
                services = _context.Service.ToList() ,
                sousServices = _context.Specialite.ToList() 


            };


        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)
            {
               
                var user = new Utilisateur
                {
                    firstName = Input.firstName,
                    lastName = Input.lastName,
                    UserName = Input.Email,
                    prestataire = Input.prestataire,
                    Email = Input.Email,
                    region = Input.region,
                    PhoneNumber = Input.PhoneNumber,
                    genre = Input.genre,
                    
                   
                };
                if (Input.role != "user")
                {
                    _logger.LogInformation($"Service: {Input.service}, Sous-service: {Input.sousservice}");

                    if (!string.IsNullOrEmpty(Input.service))
                    {
                        user.service = Input.service;
                    }

                    if (!string.IsNullOrEmpty(Input.sousservice))
                    {
                        user.souservice = Input.sousservice;
                    }
                }


                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);

                   

                        _logger.LogInformation("User created a new account with password.");
                    //ici on affecte le role a l'utilisateur crée
                    await _userManager.AddToRoleAsync(user, Input.role);
                   
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task AddServiceToUserAsync(string userId, int serviceId)
        {
            // Récupérer l'utilisateur
            var user = await _userManager.FindByIdAsync(userId);

           
        }


        private Utilisateur CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Utilisateur>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Utilisateur)}'. " +
                    $"Ensure that '{nameof(Utilisateur)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Utilisateur> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Utilisateur>)_userStore;
        }
        [HttpGet]
        public JsonResult GetSousServices(int serviceId)
        {
            var sousServices = _context.Specialite
                .Where(ss => ss.Id == serviceId)
                .Select(ss => new { id = ss.Id, nom = ss.nom }) 
                .ToList();

            return new JsonResult(sousServices);
        }

    }
}
