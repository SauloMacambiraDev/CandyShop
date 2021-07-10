using CandyShop.Data;
using CandyShop.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models.Persistences
{
    public class OrderRepository: IOrderRepository
    {
        private readonly CandyShopDbContext _dbContext;
        private readonly ShoppingCart _shoppingCart;
        public OrderRepository(CandyShopDbContext dbContext, ShoppingCart shoppingCart)
        {
            _dbContext = dbContext;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            foreach(var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    Price = shoppingCartItem.Candy.Price,
                    CandyId = shoppingCartItem.Candy.CandyId,
                    OrderId = order.OrderId
                };

                _dbContext.OrderDetails.Add(orderDetail);
            }

            _dbContext.SaveChanges();
        }
    }
}
