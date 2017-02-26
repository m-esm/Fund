using Fund.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fund.Web.Controllers
{
        [Authorize]

    public class PartnersController : Controller
    {
        //
        // GET: /Partners/

        DB db = new DB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var model = db.Partners;
            return View(model);
        }
                  [Authorize(Roles = "admin")]
        public ActionResult ToggleInvisible(int PartnerID)
        {
            var model = db.Partners.Find(PartnerID);
            if (model.Invisible)
                model.Invisible = false;
            else
                model.Invisible = true;

            db.SaveChanges();

            return Redirect("~/Partners");
        }
                  [Authorize(Roles = "admin")]
        public ActionResult Add(string PartnerName)
        {
            var ToAdd = new Partner() { Name = PartnerName, Invisible = false };
            db.Partners.Add(ToAdd);
            db.SaveChanges();
            Response.Write(ToAdd.ID);
            return new EmptyResult();

        }



    }
}
