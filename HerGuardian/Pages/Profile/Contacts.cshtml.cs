using HerGuardian.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HerGuardian.Pages.Profile
{
    public class ContactsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ContactsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<TrustedContact> Contacts { get; set; }


        [BindProperty]

        public TrustedContact NewContact { get; set; }



        public void OnGet()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            Contacts = _context.TrustedContacts.Where(c => c.UserId == userId).ToList();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);

            NewContact.UserId = userId;

            _context.TrustedContacts.Add(NewContact);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var contact = _context.TrustedContacts.Find(id);

            if (contact != null)
            {
                _context.TrustedContacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();

        }
    }
}
