using System;
using System.Reflection;
using System.IO;
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


namespace belajarRazor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private AppDbContex appDbContex;

        public ProductController(AppDbContex appDbContex)
        {
            this.appDbContex = appDbContex;
        }

        [Authorize]
        public IActionResult Index(int ? perpage, int? page, int ? order, string filter="")
        {
            int userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var items = (from i in appDbContex.Barang where i.UserID == userId select i).ToList();

            if(!string.IsNullOrEmpty(filter) || !string.IsNullOrWhiteSpace(filter))
            {
                items = (from i in items where i.name.Contains(filter) || i.description.Contains(filter) select i).ToList();
            }

            if(order != null)
            {
                items = orderBy(items, order);
            }

            ViewBag.auth = getAuth();
            ViewBag.order = order;
            ViewBag.filter = filter;
            ViewBag.perPage = perpage;

            int _perPage = 5;
            if(perpage.HasValue)
                _perPage = perpage.Value;

			var pager = new Pager(items.Count(), page, _perPage);
			
			var viewModel = new IndexViewModel
			{
				Items = items.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
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

        [Authorize]
        public IActionResult ProccessUpload([FromForm(Name="files")] IFormFile files)
        {
            int userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            Console.WriteLine(files.FileName);
            try
            {
                var streamer = new StreamReader(files.OpenReadStream());
                var str = streamer.ReadToEnd();
                var strLines = str.Split('\n');
                
                foreach (var line in strLines.Take(strLines.Count()-1))
                {
                    Console.WriteLine(line);
                    var singleData = line.Split(',');

                    var toAdd = new Barang()
                    {
                        name = singleData[0],
                        description = singleData[1],
                        img_url = singleData[2],
                        price = Convert.ToDouble(singleData[3]),
                        rating = Convert.ToInt32(singleData[4]),
                        UserID = userId,
                        createdAt = DateTime.Now,
                        editedAt = DateTime.Now
                    };

                    appDbContex.Barang.Add(toAdd);
                }

                appDbContex.SaveChanges();

                return RedirectToAction("Index", "Product");
            }
            catch (System.Exception e)
            {
                
                return Ok("format salah\n\n" + e);
            }
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
            int userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var product = new Barang()
            {
                name = _product["name"],
                description = _product["description"],
                img_url = _product["img_url"],
                price = Convert.ToDouble(_product["price"]),
                rating = Convert.ToInt32(_product["rating"]),
                createdAt = DateTime.Now,
                editedAt = DateTime.Now,
                UserID = userId
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
            string[] varList = {"name", "description", "price", "rating", "img_url"};

            var productToUpdate = appDbContex.Barang.Find(_product.id);

            foreach (var item in varList)
            {
                var prop = typeof(Barang).GetProperty(item);
                var value = prop.GetValue(_product, null);
                prop.SetValue(productToUpdate, value);
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
                return 1;

            else
                return 0;
        }
    }
}