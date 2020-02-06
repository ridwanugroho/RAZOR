using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using belajarRazor.Models;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            var items = from i in appDbContex.Barang select i;
            ViewBag.item = items;
            
            return View("Product");
        }

        public IActionResult Detail(int id)
        {
            var item = from i in appDbContex.Barang where i.id == id select i;
            
            ViewBag.item = item.First();

            return View("ProductDetail");
        }




    }
}