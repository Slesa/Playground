using Caliburn.Micro;

namespace Lucifer.Editor.ViewModel
{
    public class QuestionViewModel : Screen
    {
        public QuestionViewModel(string title, string question, params Answer[] possibleAnswers)
        {
            Title = title;
            Question = question;
            PossibleAnswers = new BindableCollection<Answer>(possibleAnswers);
        }

        public string Title { get; set; }
        public string Question { get; set; }
        public BindableCollection<Answer> PossibleAnswers { get; set; }
        public Answer? GivenAnswer { get; private set; }

        public void GiveAnswer(Answer answer)
        {
            GivenAnswer = answer;
            TryClose();
        }
    }
}