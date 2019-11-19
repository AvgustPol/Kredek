namespace Kredek.Data.ViewModels
{
    public class QuestionAndAnswerViewModel
    {
        public QuestionAndAnswerViewModel(string answer, string question)
        {
            Answer = answer;
            Question = question;
        }

        public string Answer { get; set; }
        public string Question { get; set; }
    }
}
