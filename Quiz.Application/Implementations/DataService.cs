
using Quiz.Data;

namespace Quiz.Application.Implementations
{
    public abstract class DataService
    {
        protected readonly QuizDbContext context;
        public DataService(QuizDbContext context)
        {
            this.context = context;
        }
    }
}
