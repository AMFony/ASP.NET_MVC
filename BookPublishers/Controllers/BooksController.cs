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
    public class BooksController : Controller
    {
        PublisherDbContext db = new PublisherDbContext();
        // GET: Batches
        public ActionResult Index()
        {
            return View(db.Books.Include(b => b.Authors).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateBook()
        {
            return PartialView("_CreateBook");
        }
        [HttpPost]
        public PartialViewResult CreateBook(Book b)
        {
            Thread.Sleep(3000);
            if (ModelState.IsValid)
            {
                db.Books.Add(b);
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
        public PartialViewResult EditBook(int id)
        {
            var b = db.Books.First(x => x.BookId == id);
            return PartialView("_EditBook", b);
        }
        [HttpPost]
        public PartialViewResult EditBook(Book b)
        {
            Thread.Sleep(3000);
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Books.First(x => x.BookId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int BookId)
        {
            var b = new Book { BookId = BookId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}