using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Fund.Web.Models
{
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور قبلی")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمــز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمــز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست !")]
        [Display(Name = "یوزرنیم")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]{4,20}$",
       ErrorMessage = "یوزرنیم تنها میتواند شامل A-Z , 0-9 و - _ . باشد !")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست !")]
        [StringLength(100, ErrorMessage = "حداقل {2} کارکتر !", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]{4,20}$",
      ErrorMessage = "{0} تنها میتواند شامل A-Z , 0-9 و - _ . باشد !")]
        public string Password { get; set; }


        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست !")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار پسورد")]
        [Compare("Password", ErrorMessage = "پسورد و تکرار آن مشابه نیستند !")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست !")]
        [Display(Name = "ایمیل")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
        ErrorMessage = "ایمیل معتبر نیست !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست !")]
        [Display(Name = "شماره موبایل")]
        [RegularExpression("^[0-9]{11,11}$",
ErrorMessage = "شماره موبایل نامعتبر است !")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }

    }
    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
