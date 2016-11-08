using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookBank.Models;
using BookBank.ViewModels;
namespace BookBank.Controllers
{
    public class ShoppingCartController : Controller
    {
        BookStoreEntities bookStore = new BookStoreEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel()
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
            };

            return View(viewModel);
        }
        public ActionResult AddToCart(int id)
        {
            var addedAlbum = bookStore.Books.Single(x => x.BookId == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedAlbum);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Get the name of the album to display confirmation
            //string BookName = bookStore.Carts.SingleOrDefault(item => item.BookId == id).Book.Title;
            //Remove from Cart
            int itemCount = cart.RemoveFromCart(id);
            var results = new ShoppingCartRemoveViewModel()
            {

                Message =   "Item has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id

            };
            return Json(results);
        }
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }

    }
}