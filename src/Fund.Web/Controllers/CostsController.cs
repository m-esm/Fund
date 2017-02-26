using Fund.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data;
namespace Fund.Web.Controllers
{

    static class StringExtensions
    {

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

    }


    [Authorize]
    public class CostsController : Controller
    {
        //
        // GET: /Sharjs/
        DB db = new DB();

        public ActionResult Index(int? partnerID, int page = 1)
        {

            var model = db.Costs.Where(p => p.IsCategory == false)
                .OrderByDescending(p => p.ID).AsEnumerable();

            if (partnerID.HasValue)
                model = model.Where(p => p.Percents.Any(d => d.PartnerID == partnerID.Value && d.Percent > 0)).AsEnumerable();

            return View(model.ToList().ToPagedList(page, 30));

        }

        public ActionResult Report()
        {

            var ExcelDataTable = new System.Data.DataTable("teste");


            string[] Columns = { 
                                       "ID" ,

                                       "Name" ,
                                       "Details" ,
                                       "Amount" ,
                                       "Factor DateTime" ,
                                       "Submit Date" 
                               };

            foreach (var c in Columns)
            {
                ExcelDataTable.Columns.Add(c);

            }


            foreach (var c in db.Partners)
            {
                ExcelDataTable.Columns.Add(c.Name);
                ExcelDataTable.Columns.Add(c.Name + " %");
            }

            foreach (var c in db.Costs)
            {
                List<object> row = new List<object>();
                row.Add(c.ID);
                row.Add(c.Name);
                row.Add(c.Detail);
                string amount = String.Join(",", (c.Amount).ToString().SplitInParts(3));
                row.Add(amount);



                if (c.FactorDateTime.HasValue)
                    row.Add(new m2s().miladi2shamsi(c.FactorDateTime.Value));
                else
                    row.Add("-");

                row.Add(new m2s().miladi2shamsi(c.SubmitDate));



                foreach (var partner in db.Partners)
                {
                    if (db.Percents.Any(t => t.CostID == c.ID && t.PartnerID == partner.ID))
                    {
                        float percent = db.Percents.First(t => t.CostID == c.ID && t.PartnerID == partner.ID).Percent;
                        double par = ((c.Amount * percent) / 100);
                        //string part = String.Join(",", par.SplitInParts(3));

                        row.Add((int)par);
                        row.Add(percent);



                    }
                    else
                    {
                        row.Add("-");
                        row.Add("-");
                    }


                }
                ExcelDataTable.Rows.Add(row.ToArray());
            }

            var grid = new CustomthisViewForExcelReport(ExcelDataTable, "Employees_ICT", "~/exports/");

            return new EmptyResult();

        }

        public ActionResult Categories(int page = 1)
        {

            var model = db.Costs.Where(p => p.IsCategory)
                .OrderByDescending(p => p.ID)
                .ToList().ToPagedList(page, 30);

            return View("index", model);

        }

        public ActionResult GetPercents(int? id)
        {
            if (id.HasValue == false)
                return new EmptyResult();

            var model = db.Percents.Where(p => p.CostID == id.Value).Select(p => new
            {
                partner = p.PartnerID,
                percent = p.Percent
            });

            return Json(model, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Create()
        {
            var model = new Cost();
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Cost model, string isCategory, FormCollection form, string factorDate = "")
        {
            model.SubmitDate = DateTime.Now;

            if (isCategory == "on")
                model.IsCategory = true;

            try
            {
                if (!string.IsNullOrWhiteSpace(factorDate))
                    model.FactorDateTime = m2s.shamsi2miladi(factorDate);
            }
            catch { }

            foreach (var elem in form.AllKeys.Where(p => p.ToLower().StartsWith("percent_")))
            {
                db.Percents.Add(new CostsPercents
                 {
                     CostID = model.ID,
                     PartnerID = int.Parse(elem.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1]),
                     Percent = float.Parse(form[elem])
                 });
            }


            db.Costs.Add(model);

            db.SaveChanges();

            return RedirectToAction("index");
        }


        public ActionResult Edit(int id)
        {
            var model = db.Costs.Find(id);
            return View(model);
        }



        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Cost model, string IsCategory, FormCollection form, string factorDate = "")
        {

            if (IsCategory == "on")
                model.IsCategory = true;
            else
                model.IsCategory = false;


            try
            {
                if (!string.IsNullOrWhiteSpace(factorDate))
                    model.FactorDateTime = m2s.shamsi2miladi(factorDate);
            }
            catch { }

            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            db.Percents.RemoveRange(db.Percents.Where(p => p.CostID == model.ID));

            foreach (var elem in form.AllKeys.Where(p => p.ToLower().StartsWith("percent_")))
                db.Percents.Add(new CostsPercents
                {
                    CostID = model.ID,
                    PartnerID = int.Parse(elem.Split("_".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1]),
                    Percent = int.Parse(form[elem])
                });

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var model = db.Costs.Find(id);
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
