using CandyShop.Models;
using CandyShop.Models.Interfaces;
using CandyShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(ICandyRepository candyRepository, ShoppingCart shoppingCart)
        {
            _candyRepository = candyRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(shoppingCartViewModel);
        }

        public IActionResult AddToShoppingCart(int candyId)
        {
            var candy = _candyRepository.GetCandyById(candyId);
            if(candy != null)
            {
                _shoppingCart.AddToCart(candy, 1);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromShoppingCart(int candyId)
        {
            var candy = _candyRepository.GetCandyById(candyId);
            if (candy != null)
            {
                _shoppingCart.RemoveFromCart(candy);
            }

            return RedirectToAction("Index");
        }
    }
}
