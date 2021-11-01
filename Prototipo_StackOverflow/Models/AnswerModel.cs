using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_StackOverflow.Models
{
    public class AnswerModel
    {
        public long Id { get; set; }
        public long IdQuestion { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}
