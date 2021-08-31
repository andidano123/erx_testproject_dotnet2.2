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
    [Table("QuestionTypeInfo")]
    public class QuestionTypeInfo
    {
        [Key]
        public int ID { get; set; }
        public string Title{ get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
    }
}