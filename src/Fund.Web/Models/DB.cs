using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fund.Web.Models
{
    public class DB : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Sharj> Sharjs { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<CostsPercents> Percents { get; set; }




        public DB()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CostMap());
            modelBuilder.Configurations.Add(new PartnerMap());
            modelBuilder.Configurations.Add(new SharjMap());
            modelBuilder.Configurations.Add(new CostsPercentsMap());
        }

        public DbSet<Fund.Web.Models.SharjCategory> SharjCategories { get; set; }
    
    }
}