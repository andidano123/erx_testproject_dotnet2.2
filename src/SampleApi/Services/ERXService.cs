using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleApi.Domain;

namespace SampleApi.Services
{
    public interface IERXService
    {
        Task<AccountInfo> GetUserInfo(int nUserID);
        Task<AccountInfo> GetUserInfo(string email);
        Task<List<AccountInfo>> GetAccounts();
        Task<int> AddNewAccount(AccountInfo info);
        Task AddNewAnswer(AnswerInfo info);
        Task<QuestionMoreInfo> GetQuestion(int questionid);
        Task<int> GetLastAnsweredQuestionSequence(int userid);
        Task<QuestionMoreInfo> GetNextSequenceQuestion(int sequence);
        Task<int> GetQuestionsTotalCount();
        Task<int> GetQuestionsAnswerdCount(int userid);
        //Task<QuestionCategoryInfo> GetQuestionCategory(int categoryid);
        Task UpdateAccountInfo(AccountInfo info);
        Task<List<AnswerMoreInfo>> GetAnswerListByCategoryID(int userid, int categoryid);
        Task<List<QuestionCategoryInfo>> GetQuestionCategoryList();

    }

    public class ERXService : IERXService
    {
        private readonly SampleDbContext _dbContext;
        private readonly ILogger<ERXService> _logger;

        public ERXService(SampleDbContext dbContext, ILogger<ERXService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<AccountInfo>> GetAccounts()
        {
            return await _dbContext.AccountInfos.ToListAsync();
        }

        public async Task<AccountInfo> GetUserInfo(int id)
        {
            return await _dbContext.AccountInfos.FirstOrDefaultAsync(s => s.UserID == id);
        }
        public async Task<AccountInfo> GetUserInfo(string email)
        {
            return await _dbContext.AccountInfos.FirstOrDefaultAsync(s => s.UserEmail == email);
        }
        //public async Task<List<Student>> GetSchoolStudents(Guid schoolId)
        //{
        //    return await _dbContext.Students.Where(s => s.SchoolId == schoolId).ToListAsync();
        //}

        public async Task<int> AddNewAccount(AccountInfo info)
        {
            
            _dbContext.AccountInfos.Add(info);
            await _dbContext.SaveChangesAsync();

            //Return lastest inserted id
            _dbContext.Entry(info);
            return info.UserID;
        }
        public async Task<int> GetLastAnsweredQuestionSequence(int userid)
        {
            var res = await _dbContext.AnswerInfos.OrderByDescending(s => s.CreatedAt).FirstOrDefaultAsync(s => s.UserID == userid);
            if (res == null) return 0;
            return res.QuestionSequence;
        }
        public async Task<QuestionMoreInfo> GetNextSequenceQuestion(int sequence)
        {
            //SELECT TOP 1 A.*, B.Title as CategoryTitle, C.Type as QuestionType, C.Content As TypeContent FROM QuestionInfo as A, QuestionCategoryInfo as B, QuestionTypeInfo as C WHERE A.Sequence > { 0} and B.ID = A.CategoryID and C.ID = A.TypeID ORDER BY A.Sequence ASC
            QuestionInfo info = await _dbContext.QuestionInfos.OrderBy(s=>s.Sequence).FirstOrDefaultAsync(s => s.Sequence > sequence);
            if (info == null) return null;
            QuestionCategoryInfo questionCategoryInfo = await _dbContext.QuestionCategoryInfos.FirstOrDefaultAsync(s => s.ID == info.CategoryID);
            QuestionTypeInfo questionTypeInfo = await _dbContext.QuestionTypeInfos.FirstOrDefaultAsync(s => s.ID == info.TypeID);
            QuestionMoreInfo res = new QuestionMoreInfo();
            res.ID = info.ID;
            res.NotAllowed = info.NotAllowed;
            res.Title = info.Title;
            res.CategoryID = info.CategoryID;
            res.TypeID = info.TypeID;
            res.Sequence = info.Sequence;
            res.CreatedAt = info.CreatedAt;

            res.CategoryTitle = questionCategoryInfo.Title;            
            res.TypeContent = questionTypeInfo.Content;
            res.QuestionType = questionTypeInfo.Type;
            return res;
        }
        public async Task<QuestionMoreInfo> GetQuestion(int questionID)
        {
            QuestionInfo info = await _dbContext.QuestionInfos.FirstOrDefaultAsync(s => s.ID == questionID);
            if (info == null) return null;
            QuestionCategoryInfo questionCategoryInfo = await _dbContext.QuestionCategoryInfos.FirstOrDefaultAsync(s => s.ID == info.CategoryID);
            QuestionTypeInfo questionTypeInfo = await _dbContext.QuestionTypeInfos.FirstOrDefaultAsync(s => s.ID == info.TypeID);
            QuestionMoreInfo res = new QuestionMoreInfo();
            res.ID = info.ID;
            res.NotAllowed = info.NotAllowed;
            res.Title = info.Title;
            res.CategoryID = info.CategoryID;
            res.TypeID = info.TypeID;
            res.Sequence = info.Sequence;
            res.CreatedAt = info.CreatedAt;

            res.CategoryTitle = questionCategoryInfo.Title;
            res.TypeContent = questionTypeInfo.Content;
            res.QuestionType = questionTypeInfo.Type;
            return res;
        }
        public async Task<int> GetQuestionsTotalCount()
        {
            var res = await _dbContext.QuestionInfos.CountAsync();            
            return res;
        }
        public async Task<int> GetQuestionsAnswerdCount(int userid)
        {
            var res = await _dbContext.AnswerInfos.CountAsync(s => s.UserID == userid);            
            return res;
        }
        public async Task AddNewAnswer(AnswerInfo info)
        {
          
            _dbContext.AnswerInfos.Add(info);
            await _dbContext.SaveChangesAsync();

            //Return lastest inserted id
            //_dbContext.Entry(info);
            //return info.UserID;
        }
        public async Task UpdateAccountInfo(AccountInfo info)
        {

            _dbContext.AccountInfos.Update(info);
            await _dbContext.SaveChangesAsync();

            //Return lastest inserted id
            //_dbContext.Entry(info);
            //return info.UserID;
        }

        public async Task<List<QuestionCategoryInfo>> GetQuestionCategoryList()
        {
            return await _dbContext.QuestionCategoryInfos.OrderBy(s=>s.Sequence).ToListAsync();
        }
        public async Task<List<AnswerMoreInfo>> GetAnswerListByCategoryID(int userid, int categoryid)
        {

            return await _dbContext.AnswerInfos.Where(s =>s.UserID == userid).Join(_dbContext.QuestionInfos, 
                answer=>answer.QuestionID, 
                question=>question.ID, 
                (answer, question)=> new AnswerMoreInfo {  
                    ID = answer.ID,
                    UserID = answer.UserID,
                    QuestionID = answer.QuestionID,
                    Answer = answer.Answer,
                    CreatedAt = answer.CreatedAt,
                    QuestionSequence = answer.QuestionSequence,
                    QuestionType = answer.QuestionType,
                    QuestionTitle = question.Title,                  
                    CategoryID = question.CategoryID
                }).Where(s=>s.CategoryID == categoryid).OrderBy(s => s.QuestionSequence).ToListAsync();
        }

    }
}
