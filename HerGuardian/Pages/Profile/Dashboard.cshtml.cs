using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HerGuardian.Pages.Profile
{
    public class DashboardModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostSOSAsync()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            
            Console.WriteLine($"SOS triggered by user {userId}");

            return RedirectToPage();
        }
    }
}
