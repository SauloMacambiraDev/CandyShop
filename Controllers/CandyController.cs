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
    public class CandyController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CandyController(ICandyRepository candyRepository, ICategoryRepository categoryRepository)
        {
            _candyRepository = candyRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(string category = null)
        {
            IEnumerable<Candy> candies;
            string currentCategory;
            if (string.IsNullOrEmpty(category))
            {
                candies = _candyRepository.GetAllCandy.OrderBy(c => c.CandyId);
                currentCategory = "All Candies";
            } else
            {
                candies = _candyRepository.GetAllCandy.Where(c => c.Category.CategoryName == category).OrderBy(c => c.CandyId);
                currentCategory = _categoryRepository.GetAllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }
            
            var candyListViewModel = new CandyListViewModel(candies, currentCategory);
            return View(candyListViewModel);
        }

        public IActionResult Details(int id)
        {
            var candy = _candyRepository.GetCandyById(id);
            if (candy == null) return NotFound();

            return View(candy);
        }

        public IActionResult List(string category)
        {
            return View(category);
        }
    }
}
