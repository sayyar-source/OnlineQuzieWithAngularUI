using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quiz.Application.Interfaces;
using Quiz.Data.Models;
using Quiz.Infrastructure.ViewModels;

namespace Quiz.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService questionService;
        private readonly IQuizService quizService;
        private readonly IMapper mapper;
        public QuestionController(IQuestionService questionService, IQuizService quizService, IMapper mapper)
        {
            this.questionService = questionService;
            this.quizService = quizService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> AddQuestion()
        {
            var quizzes = (await this.quizService.AllQuizzes())
                .Select(mapper.Map<AllQuizzesViewModel>);
            ViewData["Quizzes"] = new SelectList(quizzes, "QuizUrl", "Name");
            return this.View();
        }
        [HttpGet]
        public async Task<IActionResult> DescriptionQuiz(string hrefUrl)
        {
            var result = await quizService.DescriptionQuiz( hrefUrl);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(AddQuestionBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Quizes quiz = new Quizes();
            quiz.Name = model.Name;
            quiz.Text = model.Text;
            quiz.QuizUrl = model.QuizUrl;
            var quizid= await this.quizService.CreateQuiz(quiz);
            model.Id = quizid;
            for(int i=0;i<model.QuizQuestions.Count;i++)
            {
                model.QuizQuestions[i].QuizId = quizid;
            }           
            var quizquestion = mapper.Map<Quizes>(model);        
            await this.questionService.AddQuestion(quizquestion);
            return RedirectToAction("StartQuiz", "Quiz", new { id = model.Id });
        }
    }
}