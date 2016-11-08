using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookBank.Models;
using BookBank.ViewModels;
using System.Data.Entity.Validation;
using System.IO;

namespace BookBank.Controllers
{
    [Authorize]
    public class BooksStoreManagerController : Controller
    {
        private BookStoreEntities db = new BookStoreEntities();

        // GET: BooksStoreManager
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Genre);
            return View(books.ToList());
        }

        // GET: BooksStoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: BooksStoreManager/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "Name");
            return View();
        }



        // POST: BooksStoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        public ActionResult Create(BooksViewModel model, HttpPostedFileBase upload)
        {
            
          
            if (ModelState.IsValid)
            {
                string fileName=null;
                if (upload!=null && upload.ContentLength > 0)
                {
                     fileName= Path.GetFileName(upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    upload.SaveAs(path);
                }
                ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
                var book = new Book()
                {
                    Title = model.Book.Title,
                    Price = model.Book.Price,
                    AlbumArtUrl = fileName,
                    GenreID = model.Book.GenreID
                };
                db.Books.Add(book);
                db.SaveChanges();
                var author = new Author()
                {
                    Name = model.Author.Name
                };
                db.Authors.Add(author);
                db.SaveChanges();
                int authorid = author.AuthorId;
                int bookid = book.BookId;
                var release = new Release()
                {
                    AuthorId = authorid,
                    BookId = bookid,
                    ReleaseDate = model.ReleaseDate.ReleaseDate


                };
                db.Releases.Add(release);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult NewGenre()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewGenre(string Name)
        {
            var gen = new Genre();
            gen.Name = Name;

            db.Genres.Add(gen);
            db.SaveChanges();
            return RedirectToAction("Create");
        }
        public ActionResult NewAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAuthor(string Name)
        {
            var author = new Author();
            author.Name = Name;

            db.Authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreId", "Name", book.GenreID);
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,Price,AlbumArtUrl,GenreID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreId", "Name", book.GenreID);
            return View(book);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        // POST: BooksStoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          
            
            Book book = db.Books.Find(id);
            
             db.Books.Remove(book);
             db.SaveChanges();
               
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
       

        // GET: BooksStoreManager/Edit/5
      

        // POST: BooksStoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       

        // GET: BooksStoreManager/Delete/5
       

       

      
    }



