using CandyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.ViewModels
{
    public class CandyListViewModel
    {
        public IEnumerable<Candy> Candies { get; set; }
        public string CurrentCategory { get; set; }

        public CandyListViewModel(){}

        public CandyListViewModel(IEnumerable<Candy> candies)
        {
            Candies = candies;
        }

        public CandyListViewModel(IEnumerable<Candy> candies, string currentCategory)
        {
            Candies = candies;
            CurrentCategory = currentCategory;
        }
    }
}
