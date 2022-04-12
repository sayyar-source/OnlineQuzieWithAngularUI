using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Application.Interfaces;
using Quiz.Infrastructure.ViewModels;

namespace Quiz.Web.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly IQuizService quizService;
        private readonly IMapper mapper;

        public QuizController(IQuizService quizService, IMapper mapper)
        {
            this.quizService = quizService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateQuiz()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
       

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            await this.quizService.DeleteQuiz(id);
            return RedirectToAction("AllQuizzes"); 
        }

        [HttpGet]
        public async Task<IActionResult> StartQuiz(Guid id)
        {
            var getQuiz = await this.quizService.GetQuizById(id);

            var quiz = mapper.Map<QuizViewModel>(getQuiz);

            if (quiz == null)
            {
                return NotFound();
            }
            return this.View(quiz);
        }
        [HttpGet]
        public async Task<IActionResult> GetQuizById(Guid quizId)
        {
            var getQuiz = await this.quizService.GetQuizById(quizId);

            var quiz = mapper.Map<QuizViewModel>(getQuiz);

            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        [HttpGet]
        public async Task<IActionResult> AllQuizzes()
        {
            var quizzes = (await this.quizService.GetAllQuizzes())
                .Select(mapper.Map<AllQuizzesViewModel>);
            return View(quizzes);
        }
      

    }
}