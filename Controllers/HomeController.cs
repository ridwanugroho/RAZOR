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

        public IActionResult Index(int ? perpage, int? page, int ? order, string filter="")
        {
            var items1 = new List<Barang>();

            if(!string.IsNullOrEmpty(filter) || !string.IsNullOrWhiteSpace(filter))
            {
                var products = from i in appDbContex.Barang where i.name.Contains(filter) || i.description.Contains(filter) where i.rating>5 select i;
                items1 = products.ToList();
            }

            else
            {
                var products = from i in appDbContex.Barang where i.rating>5 select i;
                items1 = products.ToList();
            }

            if(order != null)
            {
                items1 = ProductController.orderBy(items1, order);
            }

            ViewBag.auth = getAuth();
            ViewBag.order = order;
            ViewBag.filter = filter;
            ViewBag.perPage = perpage;

            int _perPage = 5;
            if(perpage.HasValue)
                _perPage = perpage.Value;

			var pager = new Pager(items1.Count(), page, _perPage);
			
			var viewModel = new IndexViewModel
			{
				Items = items1.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
				Pager = pager
			};
			
			return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return Redirect("~/Admin/Product");
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
