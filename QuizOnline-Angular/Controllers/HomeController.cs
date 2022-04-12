using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quiz.Application.Implementations;
using Quiz.Application.Interfaces;
using Quiz.Data.Models;
using Quiz.Data.Models.Dtos;
using Quiz.Infrastructure.ViewModels;
using ReflectionIT.Mvc.Paging;


namespace Quiz.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IQuizService quizService;
        private readonly IJWTService _jWTService;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, IQuizService quizService, IJWTService jWTService)
        {
           
            this.quizService = quizService;
            _logger = logger;
            _jWTService = jWTService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var data = TempData["authorized"];
            ViewBag.authorized = data;
            var quizzes = (await this.quizService.GetAllQuizzes())
                .Select(_mapper.Map<AllQuizzesViewModel>);
            var pagedQuizzes = PagingList.Create(quizzes, 8, page);
           return View(pagedQuizzes);
          

        }
      
        public  IActionResult Test()
        {
            return View();

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel userModel)
        {
            try
            {
                var user = _jWTService.GetUser(userModel);
                if(user!=null)
                {
                 
                    var token = _jWTService.Authenticate(user);

                    if (token == null)
                    {
                        return Unauthorized();
                    }


                    TempData["authorized"] = true;

                }

                return RedirectToAction("Index");

            }
            catch (System.Exception)
            {

                throw;
            }
            

        }
    }
}
