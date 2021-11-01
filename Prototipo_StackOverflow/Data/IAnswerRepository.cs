using Prototipo_StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_StackOverflow.Data
{
    public interface IAnswerRepository
    {
        AnswerModel GetAnswer(long id);
        List<AnswerModel> GetListAnswer();

        List<AnswerModel> GetAnswersByQuestion(long idQuestion);

        void SaveAnswer(AnswerModel customer);
    }
}
