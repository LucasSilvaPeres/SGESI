using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Interface.Helpers;
using Interface.ViewModels;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interface.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        private readonly Context _context;
        private readonly HashHelper _hashHelper;
        private readonly LoggerHelper _logger;


        public LoginModel(Context context, HashHelper hashHelper, LoggerHelper logger)
        {
            _context = context;
            _hashHelper = hashHelper;
            _logger = logger;

        }

        public IActionResult OnGet()
        {
            LoginViewModel = new LoginViewModel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                ViewData.Add("Error", "Houve um erro no envio das informações, tente novamente.");
                return Page();
            }

            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == LoginViewModel.Email);

                if (user == null)
                {
                    ViewData.Add("Error", "Usuário não encontrado.");
                    return Page();
                }

                var validated = _hashHelper.Validate(LoginViewModel.InputPassword, user.Salt, user.Password);

                if (!validated)
                {
                    ViewData.Add("Error", "Senha incorreta.");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim("Email", user.Email),
                    new Claim("Name", user.Name),
                    new Claim("UserId", user.Id.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/",
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddYears(1)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                await _logger.Log(user.Id, "Realizou o login no sistema.");

                if (!String.IsNullOrEmpty(Request.Query["ReturnUrl"]))
                {
                    return Redirect(Request.Query["ReturnUrl"]);
                }

                return Redirect("/");
            }
            catch(Exception ex)
            {
                Serilog.Log.Error(ex, $"Erro ao tentar fazer o login do usuário {LoginViewModel.Email}");
                ViewData.Add("Error", "Houve um erro no envio das informações, tente novamente");
                return Page();
            }
        }
    }
}