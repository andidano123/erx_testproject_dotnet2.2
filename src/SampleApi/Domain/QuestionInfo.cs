/*
 * 版本： 4.0
 * 日期：2017/8/7 10:50:43
 * 
 * 描述：实体类
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApi.Domain
{
    [Table("QuestionInfo")]
    public class QuestionInfo
    {
        [Key]
        public int ID { get; set; }
        public string Title{ get; set; }
        public int CategoryID{ get; set; }
        public int TypeID { get; set; }
        public int Sequence { get; set; }
        public string NotAllowed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}