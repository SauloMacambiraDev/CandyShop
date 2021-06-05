using CandyShop.Data;
using CandyShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models.Persistences
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CandyShopDbContext _dbContext;
        public CategoryRepository(CandyShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*
        public IEnumerable<Category> GetAllCategories => new List<Category>
        {
            new Category
            {
                CategoryId = 1,
                CategoryName = "Hardy Candy",
                CategoryDescription = "Awesome and Delicious Hardy Candy"
            },
            new Category
            {
                CategoryId = 2,
                CategoryName = "Chocolate Candy",
                CategoryDescription = "Scuptious and Chocolate Candy"
            },
            new Category
            {
                CategoryId = 3,
                CategoryName = "Fruit Candy",
                CategoryDescription = "Sweet and Sour Fruit Candy"
            },
        };
        */

        public IEnumerable<Category> GetAllCategories
        {
            get {
                return _dbContext.Categories;
            }
        }

    }
}
