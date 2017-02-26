using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
    public class CostsPercents
    {
        [Key]
        [ForeignKey("Cost")]
        [Column(Order = 0)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int CostID { get; set; }
        public Cost Cost { get; set; }

        [Key]
        [ForeignKey("Partner")]
        [Column(Order=1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int  PartnerID { get; set; }
        public Partner Partner { get; set; }

        public float Percent { get; set; }

    }

    public class CostsPercentsMap : EntityTypeConfiguration<CostsPercents>
    {
        public CostsPercentsMap()
        {
            this.HasRequired(p => p.Cost).WithMany(p => p.Percents).HasForeignKey(p => p.CostID);
            this.HasRequired(p => p.Partner).WithMany(p => p.Percents).HasForeignKey(p => p.PartnerID);
        }
    }
}