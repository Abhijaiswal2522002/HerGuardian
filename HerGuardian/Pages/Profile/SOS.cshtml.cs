using HerGuardian.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HerGuardian.Pages.Profile
{
    [Authorize]
    public class SOSModel : PageModel
    {
        private readonly AppDbContext _context;

        public SOSModel(AppDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task <IActionResult> OnPostAsync()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            double lat = 28.6139;
            double lng = 77.2090;
            var alert = new SOSAlert
            {
                UserId = userId,
                Message = " Emergency! I need help!",
                Latitude = lat,
                Longitude = lng
            };

            _context.SOSAlerts.Add(alert);
            await _context.SaveChangesAsync();

            var contacts = _context.TrustedContacts
                .Where(c => c.UserId == userId).
                ToList();


            foreach(var contact in contacts)
            {
                Console.WriteLine($" SOS to {contact.Name} at {lat},{lng}");
            }
            TempData["Message"] = "SOS Sent Succesfully!";
            return RedirectToPage();
        }
    }
}
