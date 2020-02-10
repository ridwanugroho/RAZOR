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
            ViewBag.auth = getAuth();

            return View("Product");
        }

        public IActionResult Detail(int id)
        {
            
            var item = from i in appDbContex.Barang where i.id == id select i;
            
            ViewBag.item = item.First();
            ViewBag.auth = getAuth();

            return View("ProductDetail");
        }  

        [Authorize]
        public IActionResult Add()
        {
            ViewBag.auth = getAuth();
            return View();
        }

        [Authorize]
        public IActionResult AddProduct(IFormCollection _product)
        {
            var product = new Barang()
            {
                name = _product["name"],
                desc = _product["desc"],
                img_url = _product["img_url"],
                price = Convert.ToDouble(_product["price"]),
                rating = Convert.ToInt32(_product["rating"]),
                createdAt = DateTime.Now,
                editedAt = DateTime.Now
            };

            appDbContex.Barang.Add(product);
            appDbContex.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var product = appDbContex.Barang.Find(id);
            ViewBag.item = product;


            return View();
        }

        [Authorize]
        public IActionResult Update(Barang _product)
        {
            string[] varList = {"name", "desc", "price", "rating", "img_url"};

            var productToUpdate = appDbContex.Barang.Find(_product.id);
            foreach (var item in varList)
            {
                var prop = typeof(Barang).GetProperty(item);
                prop.SetValue(productToUpdate, prop.GetValue(_product, null));
            }

            productToUpdate.editedAt = DateTime.Now;

            appDbContex.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var productToDel = appDbContex.Barang.Find(id);
            appDbContex.Barang.Remove(productToDel);
            appDbContex.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        private int getAuth()
        {
            if(HttpContext.Session.GetString("JWToken") != null)
                return 1;

            else
                return 0;
        }
    }
}