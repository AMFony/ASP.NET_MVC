using BookPublishers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BookPublishers.Controllers
{
    [Authorize]
    public class PublishersController : Controller
    {
        PublisherDbContext db = new PublisherDbContext();
        // GET: Qualifications
        public ActionResult Index()
        {
            return View(db.Publishers.Include(b => b.Editors).ToList());
        }
        public ActionResult Create()
        {

            return View();
        }
        public PartialViewResult CreatePublisher()
        {
            return PartialView("_CreatePublisher");
        }
        [HttpPost]
        public PartialViewResult CreatePublisher(Publisher p)
        {
            Thread.Sleep(4000);

            if (ModelState.IsValid)
            {
                db.Publishers.Add(p);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditPublisher(int id)
        {
            var q = db.Publishers.First(x => x.PublisherId == id);
            return PartialView("_EditPublisher", q);
        }
        [HttpPost]
        public PartialViewResult EditPublisher(Publisher t)
        {
            Thread.Sleep(2000);
            if (ModelState.IsValid)
            {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Publishers.First(x => x.PublisherId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            Publisher p = new Publisher { PublisherId = id };
            db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}