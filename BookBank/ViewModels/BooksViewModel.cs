using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookBank.Models;
namespace BookBank.ViewModels
{
    public class BooksViewModel
    {
        public  Book  Book{ get; set; }
        public Author Author { get; set; }
        public Release ReleaseDate { get; set; }
        public BooksViewModel()
        {

        }
        public BooksViewModel(Book book)
        {
            Book = book;
            Author = new Author();
            ReleaseDate = new Release();

        }
    }
}