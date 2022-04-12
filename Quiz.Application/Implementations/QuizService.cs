using AutoMapper;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Quiz.Application.Interfaces;
using Quiz.Data;
using Quiz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Quiz.Application.Implementations
{
    public class QuizService : DataService, IQuizService
    {
        private readonly IMapper mapper;
      
        private string baseUrl = "https://www.wired.com/";
        public QuizService(QuizDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper; 
        }
        public async Task<Guid> CreateQuiz(Quizes quiz)
        {
            quiz.Id = new Guid();
            quiz.date = DateTime.Now.ToString("yyyy-MM-dd");
            await this.context.Quizzes.AddAsync(quiz);
            await this.context.SaveChangesAsync();
           return quiz.Id;
        }
        public async Task DeleteQuiz(Guid id)
        {
            var quiz = await this.context.Quizzes.Include(q => q.QuizQuestions).FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return;
            }
            this.context.Quizzes.Remove(quiz);
            this.context.Questions.RemoveRange(quiz.QuizQuestions);
            await this.context.SaveChangesAsync();
        }
        public Task<IEnumerable<Quizes>> AllQuizzes()
        {
            var quizzes=  ParseLinks(baseUrl);
            return quizzes;
        }
        public async Task<IEnumerable<Quizes>> ParseLinks(string urlToCrawl)
        {
            List<Quizes> quizzes = new List<Quizes>();
            WebClient webClient = new WebClient();
            byte[] data = webClient.DownloadData(urlToCrawl);
            string download = Encoding.ASCII.GetString(data);
            var doc = new HtmlDocument();
            doc.LoadHtml(download);
            var Producthtml = doc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("data-section-title", "")
                .Contains("hero collage/right")).ToList();
            foreach (var item in Producthtml)
            {
                var a = item.Descendants("a").ToList();
                if (a.Count > 0)
                {
                    var href = a[0].Attributes["href"].Value;
                    var text = item.InnerText;
                    quizzes.Add(new Quizes {Id=new Guid(), Name = text, QuizUrl = href });
                }
            }
            return await Task.FromResult(quizzes);
        }
        public async Task<Quizes> GetQuizById(Guid id)
        {
            var quiz = await this.context.Quizzes.Include(q => q.QuizQuestions).FirstOrDefaultAsync(q => q.Id == id);
            return quiz;
        }
        public async Task<List<Quizes>> GetSearchingResults(string searchTerm)
        {
            return await this.context.Quizzes
                .Where(q => q.Name.Contains(searchTerm))
                .Include(q => q.QuizQuestions)
                .OrderByDescending(q => q.Name)
                .ThenByDescending(q => q.QuizQuestions.Count())
                .ToListAsync();
        }

        public async Task<string> DescriptionQuiz(string url)
        {
            try
            {
                WebClient webClient = new WebClient();

                byte[] data = webClient.DownloadData(baseUrl + url);
                string download = Encoding.ASCII.GetString(data);
                var doc = new HtmlDocument();
                doc.LoadHtml(download);
                var Producthtml = doc.DocumentNode.Descendants("article")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("article-body-component")).ToList();
                if(Producthtml.Count==0)
                {
                 var Producthtml2 = doc.DocumentNode.Descendants("div")
                                   .Where(node => node.GetAttributeValue("class", "")
                                   .Contains("listicle-item-product-component__description")).ToList();
                    Producthtml = Producthtml2;
                }
                return Producthtml[0].InnerText;
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Parameter index is out of range.", ex);
            } 
        }
        public async Task<IEnumerable<Quizes>> GetAllQuizzes()
        {
            var quizzes = await this.context.Quizzes.Include(q => q.QuizQuestions).ToListAsync();
            return quizzes;
        } 
    }
}
