using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
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

        public IActionResult Index(int ? page)
        {
            var items = from i in appDbContex.Barang where i.rating>5 select i;

            ViewBag.auth = getAuth();
			var pager = new Pager(items.Count(), page);
			
			var viewModel = new IndexViewModel
			{
				Items = items.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
				Pager = pager
			};

            ViewBag.item = viewModel.Items;
            ViewBag.pager = viewModel.Pager;

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

        private int getAuth()
        {
            Console.WriteLine("token : {0}", (HttpContext.Session.GetString("JWToken")));
            if(HttpContext.Session.GetString("JWToken") != null)
                return 1;

            else
                return 0;
        }
    }
}
