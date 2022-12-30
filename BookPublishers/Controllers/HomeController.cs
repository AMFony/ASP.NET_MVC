using BookPublishers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookPublishers.Controllers
{
    public class HomeController : Controller
    {
        PublisherDbContext db = new PublisherDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Publishers.ToList();
            return View();
        }
    }
}