using System;

namespace Quiz.Infrastructure.ViewModels
{
    public class QuestionViewModel
    {
        public Guid QuizId { get; set; }
        public string QuestionName { get; set; }
        public string FirstOption { get; set; }
        public string SecondOption { get; set; }
        public string ThirdOption { get; set; }
        public string FourthOption { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
