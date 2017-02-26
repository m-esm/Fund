using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
    public class Partner
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name="نام شریک")]
        public string Name { get; set; }


        [Display(Name="مخفی ؟")]
        public bool Invisible { get; set; }

        public virtual ICollection<Sharj> Sharjs { get; set; }
        public virtual ICollection<CostsPercents> Percents { get; set; }
        public virtual ICollection<UserProfile> RelatedPersons { get; set; }

    }

    public class PartnerMap : EntityTypeConfiguration<Partner>
    {
        public PartnerMap()
        {

            this.HasMany(p => p.RelatedPersons).WithOptional(p => p.RelatedPartner).HasForeignKey(p => p.RelatedPartnerID).WillCascadeOnDelete(false);

        }
    }
}