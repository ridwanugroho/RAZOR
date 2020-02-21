using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using belajarRazor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
// using PagedList;
using belajarRazor.Data;


namespace belajarRazor.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContex appDbContex;

        public ProductController(AppDbContex appDbContex)
        {
            this.appDbContex = appDbContex;
        }

        public IActionResult Index(int ? perpage, int? page, int ? order, string filter="")
        {
            var items1 = new List<Barang>();

            if(!string.IsNullOrEmpty(filter) || !string.IsNullOrWhiteSpace(filter))
            {
                var products = from i in appDbContex.Barang where i.name.Contains(filter) || i.description.Contains(filter) select i;
                items1 = products.ToList();
            }

            else
            {
                var products = from i in appDbContex.Barang select i;
                items1 = products.ToList();
            }

            if(order != null)
            {
                items1 = orderBy(items1, order);
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

        public IActionResult Detail(int id)
        {
            
            var item = from i in appDbContex.Barang where i.id == id select i;
            
            ViewBag.item = item.First();
            ViewBag.auth = getAuth();

            return View("ProductDetail");
        }  

        public static List<Barang> orderBy(List<Barang> products, int ? order)
        {
            switch (order)
            {
                case 1:
                    products = products.OrderBy(p=>p.price).ToList();
                    break;
                
                case 2:
                    products = products.OrderByDescending(p=>p.price).ToList();
                    break;

                case 3:
                    products = products.OrderBy(p=>p.name).ToList();
                    break;

                case 4:
                    products = products.OrderByDescending(p=>p.name).ToList();
                    break;

                case 5:
                    products = products.OrderBy(p=>p.createdAt).ToList();
                    break;

                case 6:
                    products = products.OrderByDescending(p=>p.createdAt).ToList();
                    break;

                case 7:
                    products = products.OrderBy(p=>p.editedAt).ToList();
                    break;

                case 8:
                    products = products.OrderByDescending(p=>p.editedAt).ToList();
                    break;
            }

            return products;
        }

        private int getAuth()
        {
            if(HttpContext.Session.GetString("JWToken") != null)
                return HttpContext.Session.GetInt32("id").Value;

            else
                return 0;
        }
    }
}