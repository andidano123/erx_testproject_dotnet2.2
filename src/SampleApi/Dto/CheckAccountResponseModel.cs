
using SampleApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApi.Dto
{
    public class CheckAccountResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        
        public int UserID { get; set; }
        public int NextStatus { get; set; }     // 1: Finished, 0:New or Answering
        public AccountInfoDto AccountData { get; set; }
        public QuestionInfoDto QuestionData { get; set; }
    }
}