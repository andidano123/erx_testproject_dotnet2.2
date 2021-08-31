/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:43
 * 
 * 描述：实体类
 * 
 */

using SampleApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApi.Dto
{
    /// <summary>
    /// 实体类 AccountBaseInfoDto  (属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class QuestionInfoDto
    {
        public int ID { get; set; }
        public string Title{ get; set; }
        public int CategoryID{ get; set; }
        public int TypeID { get; set; }
        public int Sequence { get; set; }
        public string NotAllowed { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CategoryTitle { get; set; }
        public int QuestionType { get; set; }
        public string TypeContent { get; set; }
        public static QuestionInfoDto Map(QuestionMoreInfo info)
        {
            return new QuestionInfoDto
            {
                ID = info.ID,
                Title = info.Title,
                CategoryID = info.CategoryID,
                TypeID = info.TypeID,
                Sequence = info.Sequence,
                NotAllowed = info.NotAllowed,
                CreatedAt = info.CreatedAt,
                CategoryTitle = info.CategoryTitle,
                QuestionType = info.QuestionType,
                TypeContent = info.TypeContent                
            };
        }

        public static List<QuestionInfoDto> Map(IEnumerable<QuestionMoreInfo> infos)
        {
            return infos.Select(Map).ToList();
        }
    }
}