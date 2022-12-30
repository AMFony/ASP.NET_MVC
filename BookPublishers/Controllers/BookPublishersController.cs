using BookPublishers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BookPublishers.Controllers
{
    [Authorize]
    public class BookPublishersController : Controller
    {
        PublisherDbContext db = new PublisherDbContext();

        public ActionResult Index()
        {
            var data = db.Publishers.Include(x => x.BookPublishers.Select(y => y.Book)).ToList();
            return View(data);
        }
        public ActionResult CreateBookPublisher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBookPublisher(Publisher p, int[] BookId)
        {
            if (ModelState.IsValid)
            {
                foreach (var i in BookId)
                {
                    p.BookPublishers.Add(new BookPublisher { BookId = i });
                }
                db.Publishers.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }
        public PartialViewResult CreateBookEntry()
        {
            ViewBag.Books = db.Books.ToList();
            return PartialView("_BookEntry");
        }
    }
}