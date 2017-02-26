
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

using Fund.Web.Models;

namespace Fund.Web.Controllers
{   


    [Authorize]
    public class SharjCategoryController : Controller
    {
        private DB context = new DB();

        //
        // GET: /SharjCategory/

        public ViewResult Index(int? page){

            var pageNumber = page ?? 1;


            return View(context.SharjCategories.Include(sharjcategory => sharjcategory.Sharjs).ToList().ToPagedList(pageNumber, 25));
       
	    }

        //
        // GET: /SharjCategory/Details/5

        public ViewResult Details(int id)
        {
            SharjCategory sharjcategory = context.SharjCategories.Single(x => x.ID == id);
            return View(sharjcategory);
        }

        //
        // GET: /SharjCategory/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SharjCategory/Create

        [HttpPost]
        public ActionResult Create(SharjCategory sharjcategory)
        {
            if (ModelState.IsValid)
            {
                context.SharjCategories.Add(sharjcategory);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(sharjcategory);
        }
        
        //
        // GET: /SharjCategory/Edit/5
 
        public ActionResult Edit(int id)
        {
            SharjCategory sharjcategory = context.SharjCategories.Single(x => x.ID == id);
            return View(sharjcategory);
        }

        //
        // POST: /SharjCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(SharjCategory sharjcategory)
        {
            if (ModelState.IsValid)
            {
                context.Entry(sharjcategory).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sharjcategory);
        }

        //
        // GET: /SharjCategory/Delete/5
 
        public ActionResult Delete(int id)
        {
            SharjCategory sharjcategory = context.SharjCategories.Single(x => x.ID == id);
            return View(sharjcategory);
        }

        //
        // POST: /SharjCategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SharjCategory sharjcategory = context.SharjCategories.Single(x => x.ID == id);
            context.SharjCategories.Remove(sharjcategory);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


