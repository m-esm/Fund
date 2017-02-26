namespace Fund.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    public sealed class Configuration : DbMigrationsConfiguration<Fund.Web.Models.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Fund.Web.Models.DB context)
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);


            string defaultUser = "developer"; string defaultPass = "0214470"; string adminRole = "admin";
            string ViewerUser = "viewer"; string ViewerPass = "arxe"; 

            if (!Roles.RoleExists(adminRole))
                Roles.CreateRole(adminRole);


            if (!WebSecurity.UserExists(defaultUser))
                WebSecurity.CreateUserAndAccount(defaultUser, defaultPass, new { @Email = "m-esm@hotmil.com", @mobile = "09128917370" });

            if (!WebSecurity.UserExists(ViewerUser))
                WebSecurity.CreateUserAndAccount(ViewerUser, ViewerPass, new { @Email = "m-esm@hotmil.com", @mobile = "09128917370" });




            if (!Roles.IsUserInRole(defaultUser, adminRole))
                Roles.AddUserToRole(defaultUser, adminRole);


            if(context.Costs.AsEnumerable().Any(p=>string.IsNullOrWhiteSpace(p.Name)))
                foreach (var cost in context.Costs.AsEnumerable().Where(p => string.IsNullOrWhiteSpace(p.Name)))
                {
                    if (!string.IsNullOrWhiteSpace(cost.Detail))
                        cost.Name = cost.Detail;
                    cost.Detail = "";
                }

            context.SaveChanges();


        }
    }
}
