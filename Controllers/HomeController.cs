using CandyShop.Models;
using CandyShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICandyRepository _candyRepository;

        public HomeController(ILogger<HomeController> logger, ICandyRepository candyRepository)
        {
            _logger = logger;
            _candyRepository = candyRepository;
        }

        public IActionResult Index()
        {
            var candies = _candyRepository.GetCandyOnSale.Take(3);
            return View(candies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
