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
            if (getAuth() == 0)
                return View("_LoginAttemp");

            int ? userId = HttpContext.Session.GetInt32("id");
            
            var userCart = from c in appDbContex.Carts where c.userID == userId.GetValueOrDefault() select c; 

            try
            {
                var  itemList = generateItemToLoad(userCart.First());
                var totalPrice = userCart.First().totalPrice;
                ViewBag.Items = itemList;
                ViewBag.totalPrice = totalPrice;
                ViewBag.auth = getAuth();

                return View("AllCarts");
            }
            catch (System.Exception)
            {
                ViewBag.Items = null;
                ViewBag.totalPrice = null;
                ViewBag.auth = getAuth();
                return View("AllCarts");                
            }
        
        }

        public IActionResult Add(int id)
        {
            if (getAuth() == 0)
                return View("_LoginAttemp");

            int? userId = HttpContext.Session.GetInt32("id");

            var item = appDbContex.Barang.Find(id);

            var itemToAdd = new Items()
            {
                Item = item,
                quantity = 1
            };

            var cartsToCheck = from c in appDbContex.Carts select c;
            if (cartContain(cartsToCheck.ToList(), userId.GetValueOrDefault()))
            {
                var userCart = (from i in cartsToCheck where i.userID == userId.GetValueOrDefault() select i).FirstOrDefault();
                
                if(itemsContain(userCart.Items, itemToAdd))
                    return View("CartConfirmNo");

                var cart = appDbContex.Carts.Find(userCart.id);

                List<Items> temp = cart.Items;
                temp.Add(itemToAdd);

                cart.Items = temp;
                cart.totalPrice = cart.Items.Select(t=>t.Item.price*t.quantity).Sum();
                appDbContex.SaveChanges();

                return View("CartConfirmYes");
            }

            else
            {
                var itemList = new List<Items>();
                itemList.Add(itemToAdd);
                var cart = new Carts()
                {
                    userID = userId.GetValueOrDefault(),
                    Items =  itemList,
                    totalPrice = item.price
                };

                appDbContex.Carts.Add(cart);
                appDbContex.SaveChanges();

                return View("CartConfirmYes");
            }
        }

        public IActionResult update(int id, int val)
        {
            Console.WriteLine("update barang dengan id : {0}\ndengan jumlah : {1}", id, val);
            int ? userId = HttpContext.Session.GetInt32("id");
            var userCart = (from c in appDbContex.Carts where c.userID == userId.GetValueOrDefault() select c).First();


            var _userCart = appDbContex.Carts.Find(userCart.id);

            List<Items> temp = _userCart.Items;
            int n = 0;
            foreach (var item in temp)
            {
                if(item.Item.id == id)
                    break;
                n++;
            }

            temp[n].quantity = val;
            _userCart.Items = temp;
            _userCart.totalPrice = _userCart.Items.Select(t=>t.Item.price*t.quantity).Sum();
            appDbContex.SaveChanges();

            Console.WriteLine("terubah menjadi qty : {0}\ndengan total harga : {1}", _userCart.Items[n].quantity, _userCart.totalPrice);

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Remove(int id)
        {
            int userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var _cart = appDbContex.Carts.SingleOrDefault(u=>u.userID == userId);
            var items = _cart.Items.ToList();
            if(items.Count() > 1)
            {
                var itemToRemove = items.SingleOrDefault(r=>r.Item.id == id);
                items.Remove(itemToRemove);
                var cartToModify = appDbContex.Carts.Find(_cart.id);
                cartToModify.Items = items;
            }

            else
                appDbContex.Carts.Remove(_cart);

            appDbContex.SaveChanges();

            return RedirectToAction("Index", "Cart");
        }

        private List<CartItemToView> generateItemToLoad(Carts carts)
        {   
            var cartList = new List<CartItemToView>();

            foreach (var item in carts.Items)
            {
                cartList.Add(new CartItemToView()
                            {
                                id = item.Item.id,
                                name = item.Item.name,
                                qty = item.quantity,
                                itemPrice = item.Item.price,
                                totItemPrice = item.Item.price * item.quantity,
                                img_url = item.Item.img_url
                            });
            }
            
            return cartList;
        }

        private double calculateTotal(List<CartItemToView> items)
        {
            var totalPrice = items.Select(t => t.totItemPrice).Sum();
            var cart = appDbContex.Carts.Find(1);
            cart.totalPrice = totalPrice;
            appDbContex.SaveChanges();

            return totalPrice;
        }

        private bool cartContain(List<Carts> carts, int userId)
        {
            foreach(var cart in carts)
            {
                if(cart.userID == userId)
                    return true;
            }

            return false;
        }

        private bool itemsContain(List<Items> items, Items item)
        {
            foreach(var i in items)
            {
                if(i.Item.id == item.Item.id)
                    return true;
            }

            return false;
        }
    
        private int getAuth()
        {
            if(HttpContext.Session.GetString("JWToken") != null)
                return HttpContext.Session.GetInt32("id").Value;

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