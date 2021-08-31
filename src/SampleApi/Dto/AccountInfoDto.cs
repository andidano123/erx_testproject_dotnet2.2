using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApi.Domain
{
    public class AccountInfoDto
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishAnswerAt { get; set; }
        public int AnswerStatus { get; set; }

        public static AccountInfoDto Map(AccountInfo info)
        {
            return new AccountInfoDto
            {
                UserID = info.UserID,
                UserEmail = info.UserEmail,
                CreatedAt = info.CreatedAt,
                FinishAnswerAt = info.FinishAnswerAt,
                AnswerStatus = info.AnswerStatus
            };
        }

        public static List<AccountInfoDto> Map(IEnumerable<AccountInfo> infos)
        {
            return infos.Select(Map).ToList();
        }

    }
}
