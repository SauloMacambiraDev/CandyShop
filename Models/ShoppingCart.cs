using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CandyShop.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        private readonly CandyShopDbContext _dbContext;

        public ShoppingCart(CandyShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var dbContext = services.GetRequiredService<CandyShopDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);


            return new ShoppingCart(dbContext)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddToCart(Candy candy, int amount)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(sci => sci.Candy.CandyId == candy.CandyId && sci.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Candy = candy,
                    Amount = amount
                };
                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
                
            } else
            {
                shoppingCartItem.Amount++;
            }

            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(Candy candy)
        {
            int localAmount = 0;

            //1) Check if ShoppingCartItem exists
            var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(s => s.Candy.CandyId == candy.CandyId && s.ShoppingCartId == ShoppingCartId);
            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                } else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _dbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? 
                (ShoppingCartItems = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                                                                .Include(s => s.Candy)
                                                                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId);
            _dbContext.ShoppingCartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _dbContext.ShoppingCartItems
                                                    .Where(s => s.ShoppingCartId == ShoppingCartId)
                                                    .Select(c => c.Candy.Price * c.Amount)
                                                    .Sum();

            return total;
        }
    }
}
