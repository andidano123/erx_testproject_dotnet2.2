
using SampleApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApi.Dto
{
    public class AnswerListResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<QuestionCategoryInfoDto> CategoryList { get; set; }

    }
}