using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Display(Name = "یوزرنیم")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "موبایل")]
        public string mobile { get; set; }

        public int? RelatedPartnerID { get; set; }

        public virtual Partner RelatedPartner { get; set; }

    }
}