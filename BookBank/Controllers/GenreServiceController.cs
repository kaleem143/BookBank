using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookBank.Models;
namespace BookBank.Controllers
{

    public class GenreServiceController : ApiController
    {
        BookStoreEntities db = new BookStoreEntities();
        public IEnumerable<Genre> Get()
        {
           
                return db.Genres.ToList();
            
        }
        public IEnumerable<Genre> Get(int id)
        {

            return db.Genres.Where(x=>x.GenreId==id);

        }

    }
}
