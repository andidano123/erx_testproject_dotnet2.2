using SampleApi.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SampleApi.Dto
{
    public class AnswerInfoDto
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedAt { get; set; }
        public string QuestionTitle { get; set; }
        public int QuestionSequence { get; set; }
        public int QuestionType { get; set; }

        public static AnswerInfoDto Map(AnswerMoreInfo info)
        {
            return new AnswerInfoDto
            {
                ID = info.ID,
                UserID = info.UserID,
                QuestionID = info.QuestionID,
                Answer = info.Answer,
                CreatedAt = info.CreatedAt,
                QuestionSequence = info.QuestionSequence,
                QuestionType = info.QuestionType,
                QuestionTitle = info.QuestionTitle
            };
        }
        public static List<AnswerInfoDto> Map(IEnumerable<AnswerMoreInfo> infos)
        {
            return infos.Select(Map).ToList();
        }
    }
}
