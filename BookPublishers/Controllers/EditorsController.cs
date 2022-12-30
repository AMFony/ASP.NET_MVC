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
    public class EditorsController : Controller
    {
        PublisherDbContext db = new PublisherDbContext();

        public ActionResult Index()
        {
            return View(db.Editors.Include(x => x.Publisher).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateEditor()
        {
            ViewBag.Publisher = db.Publishers.ToList();
            return PartialView("_CreateEditor");
        }
        [HttpPost]
        public PartialViewResult CreateEditor(Editor f)
        {
            Thread.Sleep(3000);

            if (ModelState.IsValid)
            {
                db.Editors.Add(f);
                db.SaveChanges();
                return PartialView("_Success");
            }
            ViewBag.Publisher = db.Publishers.ToList();
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditEditor(int id)
        {
            ViewBag.Publisher = db.Publishers.ToList();
            var p = db.Editors.First(x => x.EditorId == id);
            return PartialView("_EditEditor", p);
        }
        [HttpPost]
        public PartialViewResult EditEditor(Editor f)
        {
            Thread.Sleep(3000);
            if (ModelState.IsValid)
            {
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            ViewBag.Publisher = db.Publishers.ToList();
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Editors.First(x => x.EditorId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            Editor t = new Editor { EditorId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}