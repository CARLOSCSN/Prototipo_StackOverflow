using Prototipo_StackOverflow.Models;
using System.IO;
using System.Linq;
using Dapper;

namespace Prototipo_StackOverflow.Data
{
    public class SqLiteQuestionRepository : SqLiteBaseRepository, IQuestionRepository
    {
        public QuestionModel GetQuestion(long id)
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                QuestionModel result = cnn.Query<QuestionModel>(
                    @"SELECT Id, Title, Body, Date
                    FROM Question
                    WHERE Id = @id", new { id }).FirstOrDefault();
                return result;
            }
        }

        public void SaveQuestion(QuestionModel Question)
        {
            if (!File.Exists(DbFile))
            {
                CreateDatabase();
            }

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                Question.Id = cnn.Query<long>(
                    @"INSERT INTO Question 
                    ( Title, Body, Date ) VALUES 
                    ( @Title, @Body, @Date );
                    select last_insert_rowid()", Question).First();
            }
        }

        private static void CreateDatabase()
        {
            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                cnn.Execute(
                    @"create table IF NOT EXISTS Question
                      (
                         ID                              integer primary key AUTOINCREMENT,
                         Title                           varchar(100) not null,
                         Body                            varchar(900) not null,
                         Date                            datetime not null
                      )");
            }
        }
    }
}
