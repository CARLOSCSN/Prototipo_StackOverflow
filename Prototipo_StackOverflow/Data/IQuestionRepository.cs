using Prototipo_StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_StackOverflow.Data
{
    public interface IQuestionRepository
    {
        QuestionModel GetQuestion(long id);
        List<QuestionModel> GetListQuestion();

        void SaveQuestion(QuestionModel customer);

        void CreateDatabase();
    }
}
