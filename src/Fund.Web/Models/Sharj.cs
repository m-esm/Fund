using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
    public class Sharj
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "مبلغ (ریال)")]
        public int Amount { get; set; }


        [Display(Name = "شریک")]
        public int PartnerID { get; set; }

        public virtual Partner Partner { get; set; }

        [Display(Name = "سند انتقال")]
        public string TransferDocumentPath { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime SubmitDate { get; set; }

        [Display(Name = "شرح")]
        public string Description { get; set; }
        [Display(Name = "هزینه مرتبط")]

        public int? RelatedCost { get; set; }

          [Display(Name = "دسته شارژ")]
        public int? CategoryID { get; set; }
        public virtual SharjCategory Category { get; set; }

    }

    public class SharjMap : EntityTypeConfiguration<Sharj>
    {
        public SharjMap()
        {
            this.HasRequired(p => p.Partner).WithMany(p => p.Sharjs).HasForeignKey(p => p.PartnerID).WillCascadeOnDelete(false);
            this.HasOptional(p => p.Category).WithMany(p => p.Sharjs).HasForeignKey(p => p.CategoryID);


        }
    }
}