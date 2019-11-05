using EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Threading.Tasks;

namespace Kredek.Pages.Contact
{
    [BindProperties]
    public class ContactModel : PageModel
    {
        public string FromFirstName { get; set; }
        public string FromLastName { get; set; }
        public string FromLastIndex { get; set; }
        public string FromEmail { get; set; }
        public string FromText { get; set; }
        public string FromSubjectTag { get; set; }
        public string FromSubject { get; set; }

        private readonly IEmailService _emailService;

        public ContactModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task OnGetAsync()
        {
        }

        private string MakeBold(string text)
        {
            return $"<b>{text}</b>";
        }

        private void CreateFullMessage()
        {
            StringBuilder builder = new StringBuilder();

            string boldSubjectTag = MakeBold($"[ {FromSubjectTag} ]");

            builder.Append($"{boldSubjectTag} {FromSubject}")
                .AppendLine()
                .AppendLine();

            builder
                .Append($"{MakeBold("Imię:")} {FromFirstName}").AppendLine();
            builder
                .Append($"{MakeBold("Nazwisko:")} {FromLastName}").AppendLine();

            builder
                .Append($"{MakeBold("Treść:")} {FromText}").AppendLine();

            string result = builder.ToString();
        }

        public async Task OnPostAsync()
        {
            CreateFullMessage();
        }
    }
}