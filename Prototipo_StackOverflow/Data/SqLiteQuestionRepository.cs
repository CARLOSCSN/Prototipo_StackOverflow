using Prototipo_StackOverflow.Models;
using System.IO;
using System.Linq;
using Dapper;
using System.Collections.Generic;

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

        public List<QuestionModel> GetListQuestion()
        {
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                cnn.Open();
                var result = cnn.Query<QuestionModel>(
                    @"SELECT Id, Title, Body, Date
                    FROM Question
                    WHERE 1 = 1").ToList();

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

        public void CreateDatabase()
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
                      );

                      create table IF NOT EXISTS Answer
                      (
                         ID                              integer primary key AUTOINCREMENT,
                         IdQuestion                      integer not null,
                         Title                           varchar(100) not null,
                         Body                            varchar(900) not null,
                         Date                            datetime not null
                      );");
            }
        }
    }
}
