using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebKorpa.Data;
using WebKorpa.Models;

namespace WebKorpa.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ProdavnicaContext db;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ProdavnicaContext _db
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            db = _db;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Unesi email adresu")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Unesi ime")]
            [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
            public string Ime { get; set; }

            [Required(ErrorMessage = "Unesi prezime")]
            [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
            public string Prezime { get; set; }

            [Required(ErrorMessage = "Unesi drzavu")]
            [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
            public string Drzava { get; set; }

            [Required(ErrorMessage = "Unesi grad")]
            [StringLength(30, ErrorMessage = "Maksimalno 30 karaktera")]
            public string Grad { get; set; }

            [Required(ErrorMessage = "Unesi adresu")]
            [StringLength(100, ErrorMessage = "Maksimalno 100 karaktera")]
            public string Adresa { get; set; }


            [Required(ErrorMessage = "Unesi lozinku")]
            [StringLength(100, ErrorMessage = "Izmedju 3 i 100 karaktera.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Lozinka")]
            public string Lozinka { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potvrdi lozinku")]
            [Compare("Lozinka", ErrorMessage = "Potvrda ne odgovara lozinci.")]
            public string Potvrda { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Ime = Input.Ime, Prezime = Input.Prezime, Drzava = Input.Drzava, Grad = Input.Grad, Adresa = Input.Adresa };
                var result = await _userManager.CreateAsync(user, Input.Lozinka);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    Kupac k = new Kupac
                    {
                        KupacId = user.Id,
                        Ime = user.Ime,
                        Prezime = user.Prezime,
                        Drzava = user.Drzava,
                        Grad = user.Grad,
                        Adresa = user.Adresa
                    };

                    db.Kupci.Add(k);
                    db.SaveChanges();


                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
