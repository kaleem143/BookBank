using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookBank.Models;

namespace BookBank.Controllers
{
    public class BookStoreController : Controller
    {
        // GET: BookStorebook
        BookStoreEntities db = new BookStoreEntities();

  

        public ActionResult Index()
        {
            var genre = db.Genres.ToList();
            return View(genre);
        }

        public ActionResult Browse(string genre)
        {
            var genreModel = db.Genres.Include("Books")
                      .Single(g => g.Name == genre);
            return View(genreModel);
        }
        public ActionResult Details(int id)
        {
            var book = db.Books.Find(id);
            return View(book);
        }
    }
}