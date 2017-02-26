using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
   
    public class Cost
    {
        public Cost()
        {
            Percents = new List<CostsPercents>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Display(Name = "شرح")]
        public string Detail { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "مبلغ (ریال)")]
        public int Amount { get; set; }

        public DateTime? FactorDateTime { get; set; }

        [Display(Name = "فاکتور")]

        public string FactorDocumentPath { get; set; }
        [Display(Name = "سند خرید")]
        public string PaymentDocumentPath { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime SubmitDate { get; set; }

        public virtual ICollection<CostsPercents> Percents { get; set; }

        [Display(Name = "یک دسته بندی حساب شود ؟")]
        public bool IsCategory { get; set; }


        [Display(Name = "دسته هزینه")]
        public int? CategoryID { get; set; }


      

    }

    public class CostMap : EntityTypeConfiguration<Cost>
    {
        public CostMap()
        {
          
        }
    }

}