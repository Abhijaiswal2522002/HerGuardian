using HerGuardian.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HerGuardian.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AuthService _authService;

        public RegisterModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public RegisterViewModel User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var success = await _authService.Register(
                User.Name, User.Email, User.Password
            );

            if (!success)
            {
                ModelState.AddModelError("", "User already exists");
                return Page();
            }

            return RedirectToPage("/Login");
        }
    }
}
