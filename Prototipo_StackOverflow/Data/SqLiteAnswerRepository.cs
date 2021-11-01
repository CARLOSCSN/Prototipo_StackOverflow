using Prototipo_StackOverflow.Models;
using System.IO;
using System.Linq;
using Dapper;
using System.Collections.Generic;

namespace Prototipo_StackOverflow.Data
{
    public class SqLiteAnswerRepository : SqLiteBaseRepository, IAnswerRepository
    {
        public AnswerModel GetAnswer(long id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                AnswerModel result = cnn.Query<AnswerModel>(
                    @"SELECT Id, IdQuestion, Title, Body, Date
                    FROM Answer
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public List<AnswerModel> GetAnswersByQuestion(long idQuestion)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Query<AnswerModel>(
                    @"SELECT Id, IdQuestion, Title, Body, Date
                    FROM Answer
                    WHERE IdQuestion = @idQuestion", new { idQuestion }).ToList();

                return result;
            }
        }

        public List<AnswerModel> GetListAnswer()
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Query<AnswerModel>(
                    @"SELECT Id, IdQuestion, Title, Body, Date
                    FROM Answer
                    WHERE 1 = 1").ToList();

                return result;
            }
        }

        public void SaveAnswer(AnswerModel Answer)
        {
            if (!File.Exists(DbFile))
            {
                IQuestionRepository repQuestion = new SqLiteQuestionRepository();
                repQuestion.CreateDatabase();
            }

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                Answer.Id = cnn.Query<long>(
                    @"INSERT INTO Answer 
                    ( IdQuestion, Title, Body, Date ) VALUES 
                    ( @IdQuestion, @Title, @Body, @Date );
                    select last_insert_rowid()", Answer).First();
            }
        }
    }
}
