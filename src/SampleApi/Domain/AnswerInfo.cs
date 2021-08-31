using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApi.Domain
{
    [Table("AnswerInfo")]
    public class AnswerInfo
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; }
        public int QuestionSequence { get; set; }
        public int QuestionType { get; set; }
    }
}
