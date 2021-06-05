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

        public IActionResult Index()
        {

            var candyListViewModel = new CandyListViewModel(_candyRepository.GetAllCandy);
            candyListViewModel.CurrentCategory = "Bestsellers";
            return View(candyListViewModel);
        }

        public IActionResult Details(int id)
        {
            var candy = _candyRepository.GetCandyById(id);
            if (candy == null) return NotFound();

            return View(candy);
        }
    }
}
