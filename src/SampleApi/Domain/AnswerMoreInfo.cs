using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApi.Domain
{
    public class AnswerMoreInfo
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; }
        public string QuestionTitle { get; set; }
        public int CategoryID { get; set; }

        public int QuestionSequence { get; set; }
        public int QuestionType { get; set; }
    }
}
