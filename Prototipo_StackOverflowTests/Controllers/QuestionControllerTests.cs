using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prototipo_StackOverflow.Controllers;
using Prototipo_StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototipo_StackOverflow.Controllers.Tests
{
    [TestClass()]
    public class QuestionControllerTests
    {
        [TestMethod()]
        public void CreateQuestionTest()
        {
            try
            {
                _FacadeExample.Action.TestAllFeatures();
                Assert.IsFalse(false);
            }
            catch (Exception ex)
            {
                Assert.IsFalse(true, ex.Message);
            }
         
        }
    }
}


// Library
namespace _FacadeExample
{
    // Facade
    public static class Action
    {
        private static readonly ILogger<QuestionController> _loggerQuestion;
        private static readonly ILogger<AnswerController> _loggerAnswer;

        // Subsystem A
        static QuestionController question = new QuestionController(_loggerQuestion);

        // Subsystem B
        static AnswerController answer = new AnswerController(_loggerAnswer);

        // Subsystem C
        // Ex. Avaliar resposta

        // Subsystem C
        // Ex. Loggout


        // Operation A
        public static void TestAllFeatures()
        {
            var retrievedQuestion = question.CreateQuestionTest();

            if (retrievedQuestion.Answers == null)
                retrievedQuestion.Answers = new List<AnswerModel>();

            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                var answer = new AnswerModel {
                    Date = DateTime.Now,
                    IdQuestion = retrievedQuestion.Id,
                    Title = "Resposta " + (i + 1) + " da pergunta " + "'" + retrievedQuestion.Title + "'",
                    Body = "O Lorem Ipsum é um texto modelo da indústria tipográfica e de impressão. O Lorem Ipsum tem vindo a ser o texto padrão usado por estas indústrias desde o ano de 1500..."
                };

                retrievedQuestion.Answers.Add(answer);
            }

            answer.CreateAnswersTest(retrievedQuestion);
        }
    }
}