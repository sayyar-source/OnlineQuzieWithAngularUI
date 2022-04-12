
using System;
using System.Collections.Generic;
namespace Quiz.Infrastructure.ViewModels
{
    public class AddQuestionBindingModel
    {      
        public Guid Id { get; set; }
        public string QuizUrl { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public IList<QuestionViewModel> QuizQuestions { get; set; }
    }
}
