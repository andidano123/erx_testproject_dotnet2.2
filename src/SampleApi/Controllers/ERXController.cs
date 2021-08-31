using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SampleApi.Domain;
using SampleApi.Dto;
using SampleApi.Services;
using Willow.Infrastructure.Services;

namespace SampleApi.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class ERXController : ControllerBase
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IERXService _erxService;

        public ERXController(IDateTimeService dateTimeService, IERXService eRXService)
        {
            _dateTimeService = dateTimeService;
            _erxService = eRXService;
        }

        [HttpGet("accounts")]
        [ProducesResponseType(typeof(List<AccountInfoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _erxService.GetAccounts();
            return Ok(AccountInfoDto.Map(accounts));
        }

        //[HttpGet("schools/{schoolId}/students")]
        //[ProducesResponseType(typeof(List<StudentDto>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> GetSchoolStudents([FromRoute] Guid schoolId)
        //{
        //    var school = await _studentService.GetSchool(schoolId);
        //    if (school == null)
        //    {
        //        return NotFound();
        //    }

        //    var students = await _studentService.GetSchoolStudents(schoolId);
        //    return Ok(StudentDto.Map(students));
        //}

        [HttpPost("check_account")]
        [ProducesResponseType(typeof(CheckAccountResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            CheckAccountResponseModel res = new CheckAccountResponseModel();
            res.Code = 200;
            res.Message = "Success";

            var accountInfo = await _erxService.GetUserInfo(request.email);
            int last_question_seq = 0;

            // Already Exists, Continue or Show result
            if (accountInfo != null)
            {
                //Finish answering
                if (accountInfo.AnswerStatus == 1)
                {
                    res.NextStatus = 1;
                    res.AccountData = AccountInfoDto.Map(accountInfo);
                }
                else
                {
                    res.NextStatus = 0;
                    var last_result = await _erxService.GetLastAnsweredQuestionSequence(accountInfo.UserID);
                    last_question_seq = last_result;
                }
                res.UserID = accountInfo.UserID;
            }
            else
            {
                res.NextStatus = 0;
                last_question_seq = 0;
                AccountInfo info = new AccountInfo();
                info.AnswerStatus = 0;
                info.CreatedAt = DateTime.Now;
                info.FinishAnswerAt = null;
                info.UserEmail = request.email;
                var new_userid = await _erxService.AddNewAccount(info);                
                res.UserID = new_userid;
            }

            if (res.NextStatus == 0 && res.Code == 200)
            {
                var question_result = await _erxService.GetNextSequenceQuestion(last_question_seq);                
                // The question to answer next.
                res.QuestionData = Dto.QuestionInfoDto.Map(question_result);                
            }
            
            return Ok(res);
        }

        [HttpPost("check_answer")]
        [ProducesResponseType(typeof(CheckAnswerResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CheckAnswer([FromBody] CheckAnswerRequest request)
        {
            CheckAnswerResponseModel res = new CheckAnswerResponseModel();
            res.Code = 200;
            res.Message = "Success";


            int userid = request.userid;
            string answer = request.answer;   // if question type is multiple choice, then answer is jsonarry, and otherwise it is text answer.
            int qid = request.qid;            //question id

            var account_result = await _erxService.GetUserInfo(userid);
            int last_question_seq = 0;

            //incorrect situation
            if (account_result == null || account_result.AnswerStatus == 1
                || qid == 0 || userid == 0)
            {
                res.Code = 500;
                res.Message = "Wrong Form Data";
                return Ok(res);
            }

            var answer_count_result = await _erxService.GetQuestionsAnswerdCount(userid);
            var question_count_result = await _erxService.GetQuestionsTotalCount();
            //incorrect situation
            if (answer_count_result >= question_count_result)
            {
                res.Code = 500;
                res.Message = "Wrong Form Data";
                return Ok(res);
            }


            var question_info = await _erxService.GetQuestion(qid);
            AnswerInfo info = new AnswerInfo();
            info.UserID = userid;
            info.QuestionID = qid;
            info.Answer = answer;
            info.CreatedAt = DateTime.Now;
            info.QuestionSequence = question_info.Sequence;
            info.QuestionType = question_info.QuestionType;
            
            await _erxService.AddNewAnswer(info);
            
            //The question has non-allowed anwer.
            bool finished = false;
            if (question_info.NotAllowed != "")
            {
                //string[] non_allowed_list = question_info.NotAllowed.Split(',');
                JArray non_allowd = JArray.Parse(question_info.NotAllowed);
                if (question_info.QuestionType == 3)
                {
                    JArray answer_array = JArray.Parse(answer);
                    foreach (var jObject in non_allowd)
                    {
                        string baned_answer = jObject.ToString();
                        foreach (var item in answer_array)
                        {
                            if (baned_answer == item.ToString())
                            {
                                // Not Allowed Answer
                                finished = true;
                                break;
                            }
                        }
                    }
                }
                else if (question_info.QuestionType == 2)
                {
                    foreach (var jObject in non_allowd)
                    {
                        if (jObject.ToString() == answer)
                        {
                            // Not Allowed Answer
                            finished = true;
                            break;
                        }
                    }

                }
            }
            if (answer_count_result + 1 == question_count_result)
                finished = true;

            if (finished)
            {
                account_result.AnswerStatus = 1;
                account_result.FinishAnswerAt = DateTime.Now;
                await _erxService.UpdateAccountInfo(account_result);
                res.NextStatus = 1;
                res.AccountData = AccountInfoDto.Map(account_result);
                res.AccountData.AnswerStatus = 1;
                res.AccountData.FinishAnswerAt = account_result.FinishAnswerAt;
            }
            else
            {
                //if not finished, then select next question
                var last_result = await _erxService.GetLastAnsweredQuestionSequence(account_result.UserID);
                last_question_seq = Convert.ToInt32(last_result);
                var question_result = await _erxService.GetNextSequenceQuestion(last_question_seq);
                if (question_result == null)
                {
                    res.Code = 500;
                    res.Message = "Server Error";
                }
                else
                {
                    // The question to answer next.
                    res.QuestionData = QuestionInfoDto.Map(question_result);
                }
            }
            return Ok(res);
        }
        [HttpPost("get_answer_list")]
        [ProducesResponseType(typeof(AnswerListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAnswerList([FromBody] GetAnswerListRequest request)
        {
            AnswerListResponseModel res = new AnswerListResponseModel();
            res.Code = 200;
            res.Message = "Success";

            int userid = request.userid;

            var result = await _erxService.GetQuestionCategoryList();
            res.CategoryList = new List<QuestionCategoryInfoDto>();

            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    QuestionCategoryInfoDto info = new QuestionCategoryInfoDto();
                    info.Sequence = result[i].Sequence;
                    info.ID = result[i].ID;
                    info.Title = result[i].Title;
                    var answer_result = await _erxService.GetAnswerListByCategoryID(userid, info.ID);
                    if (answer_result != null)
                    {
                        info.AnswerList = new List<AnswerInfoDto>();
                        for (int j = 0; j < answer_result.Count; j++)
                        {

                            info.AnswerList.Add(AnswerInfoDto.Map(answer_result[j]));
                        }
                    }
                    res.CategoryList.Add(info);
                }
            }
            return Ok(res);
        }

        [HttpGet("download_csv")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> download_csv(int userid)
        {
            StringBuilder sb = new StringBuilder();
            var result = await _erxService.GetQuestionCategoryList();

            if (result != null)
            {
                for (int i = 0; i < result.Count; i++)
                {

                    var answer_result = await _erxService.GetAnswerListByCategoryID(userid, result[i].ID);
                    if (answer_result != null)
                    {
                        sb.AppendFormat(
                           "{0}{1}",
                           result[i].Title,
                           Environment.NewLine);
                        for (int j = 0; j < answer_result.Count; j++)
                        {
                            if (answer_result[j].QuestionType == 3)
                            {
                                JArray temp = JArray.Parse(answer_result[j].Answer);
                                string answer_str = "";
                                int c = 0;
                                foreach (var item in temp)
                                {
                                    answer_str = item.ToString();
                                    c++;
                                    if (c != temp.Count)
                                        answer_str += ",";
                                }
                                sb.AppendFormat("{0}.  {1}:   {2}{3}",
                                   answer_result[j].QuestionSequence,
                                   answer_result[j].QuestionTitle,
                                   answer_str,
                                   Environment.NewLine);
                            }
                            else
                            {
                                sb.AppendFormat(
                               "{0}.  {1}:   {2}{3}",
                               answer_result[j].QuestionSequence,
                               answer_result[j].QuestionTitle,
                               answer_result[j].Answer,
                               Environment.NewLine);
                            }

                        }
                        sb.AppendFormat(
                          "{0}",
                          Environment.NewLine);
                    }

                }
            }

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(sb.ToString());
            writer.Flush();
            stream.Position = 0;

            string csvName = $"Answer-{DateTime.UtcNow.Ticks}.csv";
            return File(stream.ToArray(), "text/csv", csvName);
            
        }

        public class CreateAccountRequest
        {
            public string email { get; set; }
        }
        public class GetAnswerListRequest
        {
            public int userid{ get; set; }
        }
        public class CheckAnswerRequest
        {            
            public int userid { get; set; }
            public string answer { get; set; }
            public int qid { get; set; }
        }
    }
}