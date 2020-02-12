using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
            int ? userId = HttpContext.Session.GetInt32("id");
            
            // var  itemList = generateItemToLoad(carts.First().Items.ToList());
            // var totalPrice = calculateTotal(itemList);
            // ViewBag.Items = itemList;
            // ViewBag.totalPrice = totalPrice;
            // ViewBag.auth = getAuth();

            return View("AllCarts");
        }

        public IActionResult Add(int id)
        {
            if(getAuth() == 0)
                return Ok("Anda harus login!");

            // int ? userId = HttpContext.Session.GetInt32("id");
            // var user = appDbContex.User.Find(userId);
            // var barang = appDbContex.Barang.Find(id);
            // var item = new Item()
            // {
            //     Barang = barang,
            //     qty = 1
            // };


            // if(!appDbContex.Cart.Any())
            // {
            //     var cart = new Cart()
            //     {
            //         Items = new List<Item>(){item},
            //         totalPrice = barang.price
            //     };

            //     appDbContex.Cart.Add(cart);
            //     appDbContex.SaveChanges();
            // }

            // else
            // {
            //     if(appDbContex.Items.Any(i=>i.Barang == barang))
            //         return View("CartConfirmNo");

            //     var cart = appDbContex.Cart.Find(1);
            //     cart.Items = new List<Item>(){item};
            //     cart.editedAt = DateTime.Now;
            //     appDbContex.SaveChanges();
            // }

            
            return View("CartConfirmYes");
        }

        // public IActionResult update(int id, int val)
        // {
        //     Console.WriteLine(id);
        //     Console.WriteLine(val);
        //     var item = appDbContex.Items.Find(id);
        //     item.quantity = val;
        //     appDbContex.SaveChanges();

        //     return RedirectToAction("Index", "Cart");
        // }

        public IActionResult Remove(int id)
        {
            // var itemToRemove = appDbContex.Items.Find(id);
            // appDbContex.Items.Remove(itemToRemove);
            // appDbContex.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

        // private List<CartItemToView> generateItemToLoad(List<Item> items)
        // {   
        //     var cartList = new List<CartItemToView>();

        //     foreach (var item in items)
        //     {
        //         cartList.Add(new CartItemToView()
        //                     {
        //                         id= item.id,
        //                         name = item.Barang.name,
        //                         qty = item.qty,
        //                         itemPrice = item.Barang.price,
        //                         totItemPrice = item.Barang.price * item.qty,
        //                         img_url = item.Barang.img_url
        //                     });
        //     }
            
        //     return cartList;
        // }

        private double calculateTotal(List<CartItemToView> items)
        {
            var totalPrice = items.Select(t => t.totItemPrice).Sum();
            var cart = appDbContex.Carts.Find(1);
            cart.totalPrice = totalPrice;
            appDbContex.SaveChanges();

            return totalPrice;
        }
    
        private int getAuth()
        {
            if(HttpContext.Session.GetString("JWToken") != null)
                return 1;

            else
                return 0;
        }
    }


    public class CartItemToView
    {
        public int id{get; set;}
        public string name{get; set;} = "null";
        public int qty{get; set;} = 0;
        public double itemPrice{get; set;} = 0;
        public double totItemPrice{get; set;} = 0;
        public string img_url{get; set;} = "";
    }
}