using System.Web.Mvc;

namespace Fund.Web.Areas.Filemanager
{
    public class FilemanagerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Filemanager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
          "Filemanager_default",
          "Filemanager/{action}/{id}",
          new { Controller = "Filemanager", action = "Index", id = UrlParameter.Optional }
      );
        }
    }
}
