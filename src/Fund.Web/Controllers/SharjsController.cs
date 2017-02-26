using Fund.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Fund.Web.Controllers
{
    [Authorize]
    public class SharjsController : Controller
    {
        //
        // GET: /Sharjs/
        DB db = new DB();


              [Authorize(Roles = "admin")]
        public ActionResult Sync()
        {
            Response.Write("<style>*{direction:rtl;text-align:right;font-family:tahoma;font-size:17px;}</style>");

            var sharjs = db.Sharjs.AsEnumerable();
            var costs = db.Costs.AsEnumerable();
            var partners = db.Partners.AsEnumerable();

            Dictionary<float, float> PartnersTotalSharj = new Dictionary<float, float>();


            foreach (var p in partners)
                PartnersTotalSharj.Add(p.ID, 0);

            foreach (var sharj in sharjs)
                PartnersTotalSharj[sharj.PartnerID] = sharj.Amount;

            foreach (var c in costs)
            {
                Response.Write(c.Detail +" " + c.Amount +  "ریال<br>");
                 foreach(var p in c.Percents)
                 {
                     float cash = ((c.Amount * p.Percent) / 100);
                     Response.Write(partners.First(d=>d.ID == p.PartnerID).Name + " : " + p.Percent
                         + "%"
                         + "<br>"
                         +   cash    + "Rial" 
                         + "<br>");
                     PartnersTotalSharj[p.PartnerID] -= cash;
                 }


                 Response.Write("<br>");


            }
            Response.Write("<br>");
            Response.Write("<br>");

            foreach (var s in PartnersTotalSharj)
            {
                Response.Write(s.Key + " " + s.Value + "<br>");
            }
            

            return new EmptyResult();

        }
        public ActionResult Index(int page = 1)
        {
            var model = db.Sharjs.OrderByDescending(p=>p.ID).ToList().ToPagedList(page, 30);
            return View(model);
        }
              [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }


              [HttpPost]
              [Authorize(Roles = "admin")]
        public ActionResult Create(Sharj model)
        {
            model.SubmitDate = DateTime.Now;
            db.Sharjs.Add(model);
            db.SaveChanges();
            return RedirectToAction("index");
        }

              [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var model = db.Sharjs.Find(id);
            return View(model);
        }


              [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Sharj model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var model = db.Sharjs.Find(id);
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
