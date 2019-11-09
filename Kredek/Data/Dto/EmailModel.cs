using Kredek.Models.Common.Emuns;

namespace Kredek.Data.Dto
{
    public class EmailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public SubjectTag SubjectTag { get; set; }
        public string Subject { get; set; }
    }
}
