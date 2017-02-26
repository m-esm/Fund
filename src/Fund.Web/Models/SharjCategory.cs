using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
    public class SharjCategory
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="نام کتگوری")]
        public string CategoryName { get; set; }
        [Display(Name="اولویت نمایش")]
        public int OrderId { get; set; }
           [Display(Name = "شارژ ها")]
        public virtual ICollection<Sharj> Sharjs { get; set; }
    }
}