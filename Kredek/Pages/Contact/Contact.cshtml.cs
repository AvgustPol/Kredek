using EmailService;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Kredek.Pages.Contact
{
    public class ContactModel : PageModel
    {
        private readonly IEmailService _emailService;

        public ContactModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task OnGetAsync()
        {
        }

        public async Task OnPostAsync()
        {
            //lorem ipsum example ;)
            string plainText = @"Emma,

                Quisque sit amet ultricies odio. Vivamus volutpat blandit eros ut dictum.
                Aenean finibus nec mauris at auctor.
                Integer eleifend neque a nulla consectetur consectetur.

                Quisque tempus finibus justo?

                Aliquam erat volutpat
                John <3
                ";

            _emailService.Message()
                .From("Emma", "emma.waella@gmail.com")
                .To("Perez Logan", "perez.logan.9.3@gmail.com")
                .WithSubject("It works!")
                .WithBodyPlain(plainText)
                .Send();
        }
    }
}