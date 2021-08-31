using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApi.Domain
{
    [Table("AccountInfo")]
    public class AccountInfo
    {
        [Key]
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishAnswerAt { get; set; }
        public int AnswerStatus { get; set; }
    }
}
