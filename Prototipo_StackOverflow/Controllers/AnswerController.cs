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
    public class AnswerController : Controller
    {
        private readonly ILogger<AnswerController> _logger;

        public AnswerController(ILogger<AnswerController> logger)
        {
            _logger = logger;
        }

        public IActionResult AnswerQuestion(long id)
        {
            IQuestionRepository repQuestion = new SqLiteQuestionRepository();
            var question = repQuestion.GetQuestion(id);

            return View("AnswerQuestion", question);
        }

        [HttpPost]
        public IActionResult CreateAnswer(QuestionModel model)
        {
            if (model?.Id > 0 && model?.Answers != null)
            {
                model.Answers.FirstOrDefault().Date = DateTime.Now;
                model.Answers.FirstOrDefault().IdQuestion = model.Id;
            }
            
            IAnswerRepository rep = new SqLiteAnswerRepository();
            rep.SaveAnswer(model.Answers.FirstOrDefault());

            var action = "GetById?id="+ model.Id;
            var controller = "Question";

            return Json(new { redirectToUrl = Url.Action(action, controller) });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
