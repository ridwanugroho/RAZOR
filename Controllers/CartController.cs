using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using belajarRazor.Models;
using belajarRazor.Data;

namespace belajarRazor.Controllers
{
    public class CartController : Controller
    {
        private AppDbContex appDbContex;

        public CartController(AppDbContex appDbContex)
        {
            this.appDbContex = appDbContex;
        }

        public IActionResult Index()
        {
            if(appDbContex.Cart.Any())
            {
                var cart = from _cart in appDbContex.Cart.Include(i=>i.Items).ThenInclude(b=>b.Barang) where _cart.id == 1 select _cart;
                var items = generateItemToLoad(cart.First());
                ViewBag.Items = items;
            }

            return View("allCarts");
        }

        public IActionResult Add(int id)
        {
            var barang = appDbContex.Barang.Find(id);
            var item = new Item()
            {
                Barang = barang,
                qty = 1
            };

            if(!appDbContex.Cart.Any())
            {
                var cart = new Cart()
                {
                    Items = new List<Item>(){item},
                    totalPrice = barang.price
                };

                appDbContex.Cart.Add(cart);
                appDbContex.SaveChanges();
            }

            else
            {
                if(appDbContex.Items.Any(i=>i.Barang == barang))
                    return RedirectToAction("Index", "Home");

                var cart = appDbContex.Cart.Find(1);
                cart.Items = new List<Item>(){item};
                appDbContex.SaveChanges();
            }

            
            return View("CartConfirmYes");
        }

        public IActionResult update(int id, int val)
        {
            
            return Ok();
        }

        private List<CartItemToView> generateItemToLoad(Cart cart)
        {
            
            var cartList = new List<CartItemToView>();

            var barang = cart.Items.GroupBy(x=>x.Barang);

            foreach (var b in barang)
            {
                cartList.Add(new CartItemToView()
                            {
                                id= b.Key.id,
                                name = b.Key.name,
                                qty = b.Count(),
                                totItemPrice = b.Key.price * b.Count(),
                                img_url = b.Key.img_url
                            });
            }
            
            return cartList;
        }
    }


    public class CartItemToView
    {
        public int id{get; set;}
        public string name{get; set;} = "null";
        public int qty{get; set;} = 0;
        public double totItemPrice{get; set;} = 0;
        public string img_url{get; set;} = "";
    }
}