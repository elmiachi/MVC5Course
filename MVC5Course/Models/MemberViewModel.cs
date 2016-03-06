using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage ="請設定有效的日期格式")]
        public DateTime Birthday { get; set; }
    }
}