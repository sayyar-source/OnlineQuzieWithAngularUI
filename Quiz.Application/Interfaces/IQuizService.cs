
using Quiz.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quiz.Application.Interfaces
{
    public interface IQuizService
    {
        Task<Guid> CreateQuiz(Quizes quiz);
        Task DeleteQuiz(Guid id);
        Task<IEnumerable<Quizes>> AllQuizzes();
        Task<IEnumerable<Quizes>> GetAllQuizzes();
        Task<Quizes> GetQuizById(Guid id);
        Task<List<Quizes>> GetSearchingResults(string searchTerm);
        Task<string> DescriptionQuiz(string url);
    }
}
