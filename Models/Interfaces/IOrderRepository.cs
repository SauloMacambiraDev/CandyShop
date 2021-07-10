using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
