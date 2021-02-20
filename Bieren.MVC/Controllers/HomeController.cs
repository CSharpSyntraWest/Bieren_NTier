using Bieren.MVC.Utils;
using Bieren.MVC.ViewModels;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bieren.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BierenDbContext _context;

        public HomeController(ILogger<HomeController> logger,BierenDbContext context)
        {
            _logger = logger;
            _context = context;

            //_context.GenerateSampleData();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(int page = 1)
        {
            var dataPage = _context.Bieren.GetPaged(page, 10);

            return View(dataPage);
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
