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

        private string CreateHtmlMessage()
        {
            string htmlLineBreak = "<br/>";
            StringBuilder builder = new StringBuilder();

            #region Title

            builder.Append($"<h2>Tytuł wiadomości</h2>")
                .AppendLine();

            builder.Append($"{MakeBold("Kategorija: ")} {FromSubjectTag}")
                .AppendLine(htmlLineBreak);

            builder.Append($"{MakeBold("Tytuł: ")} {FromSubject}")
                .AppendLine(htmlLineBreak);

            #endregion Title

            #region Contact data

            builder.AppendLine().Append($"<h2>Dane kontaktowe </h2>")
                .AppendLine();

            builder
                .Append($"{MakeBold("Imię:")} {FromFirstName}")
                .AppendLine(htmlLineBreak);
            builder
                .Append($"{MakeBold("Nazwisko:")} {FromLastName}")
                .AppendLine(htmlLineBreak);
            builder
                .Append($"{MakeBold("Email:")} {FromEmail}")
                .AppendLine()
                .AppendLine(htmlLineBreak);

            #endregion Contact data

            #region Main message body

            builder.AppendLine().Append($"<h2>Treść: </h2>")
                .AppendLine();

            string text = FromText.Replace("\r\n", htmlLineBreak);

            builder
                .Append($"{text}");

            #endregion Main message body

            return builder.ToString();
        }

        public async Task OnPostAsync()
        {
            _emailService.Message()
                .FromServer()
                .To("Test", "vlasiuk.anton@gmail.com")
                .WithSubject(FromSubject)
                .WithBodyHtml(CreateHtmlMessage())
                    .Send();
        }
    }
}