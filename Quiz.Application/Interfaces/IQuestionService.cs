

using Quiz.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Application.Interfaces
{
    public interface IQuestionService
    {
        Task AddQuestion(Quizes quizquestion);
    }
}
