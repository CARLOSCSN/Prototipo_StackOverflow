using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prototipo_StackOverflow.Data;
using Prototipo_StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_StackOverflow.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //CreateTest();
            return View();
        }

        public IActionResult GetById(long id)
        {
            IQuestionRepository rep = new SqLiteQuestionRepository();
            var result = rep.GetQuestion(id);

            if (result != null)
            {
                IAnswerRepository repAnswer = new SqLiteAnswerRepository();
                result.Answers = repAnswer.GetAnswersByQuestion(id);
            }

            return View("Detail", result);
        }

        [HttpPost]
        public IActionResult CreateQuestion(QuestionModel model)
        {
            IQuestionRepository rep = new SqLiteQuestionRepository();
            model.Date = DateTime.Now;

            rep.SaveQuestion(model);

            var action = "Index";
            var controller = "Home";
            return Json(new { redirectToUrl = Url.Action(action, controller) });
        }

        public QuestionModel CreateQuestionTest() {

            IQuestionRepository rep = new SqLiteQuestionRepository();
            Random rnd = new Random();
            var question = new QuestionModel
            {
                Title = "gerada por Teste de FACADE code: " + rnd.Next(),
                Body = "O Lorem Ipsum é um texto modelo da indústria tipográfica e de impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado por estas indústrias desde o ano de 1500...",
                Date = DateTime.Now
            };

            rep.SaveQuestion(question);
            var retrievedQuestion = rep.GetQuestion(question.Id);

            return retrievedQuestion;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
