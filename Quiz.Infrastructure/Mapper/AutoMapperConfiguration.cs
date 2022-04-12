using AutoMapper;
using Quiz.Data.Models;
using Quiz.Data.Models.Dtos;
using Quiz.Infrastructure.ServiceModels;
using Quiz.Infrastructure.ViewModels;
using Quiz.Models.Dtos;

namespace Quiz.Infrastructure.Mapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            this.CreateMap<User, UserForRegistrationDto>().ReverseMap();
            this.CreateMap<User, LoginViewModel>().ReverseMap();
            this.CreateMap<Quizes, QuizViewModel>().ReverseMap();
            this.CreateMap<Quizes, AllQuizzesViewModel>().ReverseMap();
            this.CreateMap<Question, QuestionViewModel>().ReverseMap();
            this.CreateMap<Quizes, AddQuestionBindingModel>().ReverseMap();
            this.CreateMap<QuizServiceModel, QuizViewModel>().ReverseMap();
            this.CreateMap<AnswersServiceModel, AnswersBindingModel>().ReverseMap();            
        }
    }
}