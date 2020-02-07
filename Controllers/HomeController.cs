using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using belajarRazor.Models;
using belajarRazor.Data;

namespace belajarRazor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContex appDbContex;

        public HomeController(ILogger<HomeController> logger, AppDbContex appDbContex)
        {
            this.appDbContex = appDbContex;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var items = from i in appDbContex.Barang where i.rating>6 select i;
            ViewBag.item = items;
            
            return View();
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
