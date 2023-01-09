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
    public class RegisterModel : PageModel
    {
        private readonly Context _context;
        private readonly HashHelper _hashHelper;
        private readonly LoggerHelper _logger;


        public RegisterModel(Context context, HashHelper hashHelper, LoggerHelper logger)
        {
            _context = context;
            _hashHelper = hashHelper;
            _logger = logger;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                ViewData.Add("Error", "Houve um erro no envio das informações, tente novamente.");
                return Page();
            }

            return Page();
        }
    }
}