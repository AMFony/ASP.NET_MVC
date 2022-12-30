using BookPublishers.Models;
using BookPublishers.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookPublishers.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        // GET: Authors

        PublisherDbContext db = new PublisherDbContext();

        public ActionResult Index()
        {
            return View(db.Authors.Include(x => x.Book).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Book = db.Books.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(AuthorInputModel t)
        {
            if (ModelState.IsValid)
            {
                var a = new Author
                {
                    AuthorName = t.AuthorName,
                    MobileNo = t.MobileNo,
                    BookId = t.BookId
                };
                string ext = Path.GetExtension(t.picture.FileName);
                string f = Guid.NewGuid() + ext;
                t.picture.SaveAs(Server.MapPath("~/Assets/") + f);
                a.picture = f;
                db.Authors.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Book = db.Books.ToList();
            return View(t);
        }
        public ActionResult Edit(int id)
        {
            var e = db.Authors.First(x => x.AuthorId == id);
            ViewBag.Book = db.Books.ToList();
            ViewBag.CurrentPic = e.picture;
            return View(new AuthorEditModel { AuthorId = e.AuthorId, AuthorName = e.AuthorName, MobileNo = e.MobileNo, BookId = e.BookId });
        }
        [HttpPost]
        public ActionResult Edit(AuthorEditModel e)
        {
            var t = db.Authors.First(x => x.AuthorId == e.AuthorId);
            if (ModelState.IsValid)
            {
                t.AuthorName = e.AuthorName;
                t.MobileNo = e.MobileNo;
                t.BookId = e.BookId;
                if (e.picture != null)
                {
                    string ext = Path.GetExtension(e.picture.FileName);
                    string f = Guid.NewGuid() + ext;
                    e.picture.SaveAs(Server.MapPath("~/Assets/") + f);
                    t.picture = f;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Book = db.Books.ToList();
            ViewBag.CurrentPic = t.picture;
            return View(e);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Authors.Include(x => x.Book).First(x => x.AuthorId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            Author e = new Author { AuthorId = id };
            db.Entry(e).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}