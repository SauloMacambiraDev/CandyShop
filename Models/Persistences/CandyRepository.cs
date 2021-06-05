using CandyShop.Data;
using CandyShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models.Persistences
{
    public class CandyRepository : ICandyRepository
    {
        //OLD APPROACH
        //private readonly ICategoryRepository _categoryRepository;

        //public CandyRepository(ICategoryRepository categoryRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //}

        //NEW APPROACH USING ENTITY FRAMEWORK
        private readonly CandyShopDbContext _dbContext;
        public CandyRepository(CandyShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*
        public IEnumerable<Candy> GetAllCandy => new List<Candy>
        {
            new Candy
            {
                CandyId = 1,
                Name = "Assorted Hard Candy",
                Price = 4.95m,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam ultricies odio congue sapien blandit, et tempus odio rhoncus. Suspendisse potenti. Morbi quis blandit magna, sit amet viverra lacus. Donec sed lectus eu erat lacinia commodo. Nunc pulvinar in eros ac vestibulum. In dolor eros, vulputate blandit sodales ac, elementum a nunc. Phasellus faucibus vehicula nisi tempor ornare. Ut efficitur mollis metus, a sollicitudin lectus tempus id. Mauris non sapien dui. Aliquam sed maximus libero. Morbi a ipsum mattis, vestibulum arcu sit amet, porttitor mass",
                Category = _categoryRepository.GetAllCategories.ToList()[0],
                ImageUrl = "https://images.unsplash.com/photo-1576712967455-c8d22580e9be?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1500&q=80",
                ImageThumbnailUrl = "https://images.unsplash.com/photo-1576712967455-c8d22580e9be?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1500&q=80",
                IsInStock = true,
                IsOnSale = false
            },
            new Candy
            {
                CandyId = 2,
                Name = "Cacau Candy",
                Price = 10.95m,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam ultricies odio congue sapien blandit, et tempus odio rhoncus. Suspendisse potenti. Morbi quis blandit magna, sit amet viverra lacus. Donec sed lectus eu erat lacinia commodo. Nunc pulvinar in eros ac vestibulum. In dolor eros, vulputate blandit sodales ac, elementum a nunc. Phasellus faucibus vehicula nisi tempor ornare. Ut efficitur mollis metus, a sollicitudin lectus tempus id. Mauris non sapien dui. Aliquam sed maximus libero. Morbi a ipsum mattis, vestibulum arcu sit amet, porttitor mass",
                Category = _categoryRepository.GetAllCategories.ToList()[1],
                ImageUrl = "https://images.unsplash.com/photo-1576712967455-c8d22580e9be?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1500&q=80",
                ImageThumbnailUrl = "https://images.unsplash.com/photo-1576712967455-c8d22580e9be?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1500&q=80",
                IsInStock = false,
                IsOnSale = false
            },
            new Candy
            {
                CandyId = 3,
                Name = "Watermelon Sugar Candy",
                Price = 15.95m,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus iaculis efficitur metus ut pellentesque. Pellentesque magna tellus, tincidunt accumsan posuere in, pellentesque id lectus. Sed ultrices, magna id laoreet maximus, dui ante vehicula libero, eu interdum augue leo a nisi. Vivamus ac ipsum vel quam gravida porttitor vitae at felis.",
                Category = _categoryRepository.GetAllCategories.ToList()[2],
                ImageUrl = "https://images.unsplash.com/photo-1576712967455-c8d22580e9be?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1500&q=80",
                ImageThumbnailUrl = "https://images.unsplash.com/photo-1576712967455-c8d22580e9be?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1500&q=80",
                IsInStock = true,
                IsOnSale = true
            },

        };
        */

        public IEnumerable<Candy> GetAllCandy
        {
            get {
                return _dbContext.Candies.Include(c => c.Category);
            }
        }

        public IEnumerable<Candy> GetCandyOnSale => GetAllCandy.Where(c => c.IsOnSale == true);

        public Candy GetCandyById(int candyId) => GetAllCandy.FirstOrDefault(candy => candy.CandyId == candyId);
    }
}
