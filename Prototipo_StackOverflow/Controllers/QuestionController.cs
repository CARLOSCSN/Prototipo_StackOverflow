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
            CreateTest();
            return View();
        }

        private bool CreateTest() {

            IQuestionRepository rep = new SqLiteQuestionRepository();
            var question = new QuestionModel
            {
                Title = "Error 404",
                Body = "Error 404 in API after submit form",
                Date = DateTime.Now
            };

            rep.SaveQuestion(question);
            var retrievedQuestion = rep.GetQuestion(question.Id);

            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
