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
    public class SalesController : Controller
    {
        private AppDbContex appDbContex;

        public SalesController(AppDbContex appDbContex)
        {
            this.appDbContex = appDbContex;
        }

        public IActionResult Index(int ? ByBuyer, string singleItem)
        {
            var adminId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            
            var adminSoldList = getAll(adminId);

            if(ByBuyer.HasValue)
                adminSoldList = orderByBuyer(adminSoldList, ByBuyer.Value);

            if(!string.IsNullOrEmpty(singleItem) && !string.IsNullOrWhiteSpace(singleItem))
                adminSoldList = filterPerItem(adminSoldList, singleItem);

            ViewBag.auth = getAuth();
            
            return View(adminSoldList);
        }

        private List<Sales> getAll(int adminId)
        {
            var allPurchases = from p in appDbContex.Purchases.Include(t=>t.TransactionsDetail)
                                .Include(u=>u.User) select p;

            var adminSoldList = new List<Sales>();

            foreach (var purchase in allPurchases)
            {
                foreach (var item in purchase.ItemsDetail.Items)
                {
                    if(item.Item.UserID == adminId)
                    {
                        var temp = new Sales()
                        {
                            Item = item.Item,
                            Buyer = purchase.User,
                            OrderID = purchase.TransactionsDetail.order_id,
                            Quantity = item.quantity,
                            TransactionTime = purchase.TransactionsDetail.transaction_time,
                            TransactionStatus = purchase.TransactionsDetail.transaction_status
                        };

                        adminSoldList.Add(temp);
                    }
                }
            }

            return adminSoldList;
        }
    
        private List<Sales> orderByBuyer(List<Sales> sales, int order)
        {
            if(order == 1)
                sales = sales.OrderBy(b=>b.Buyer).ToList();

            else if(order == 2)
                sales = sales.OrderByDescending(b=>b.Buyer).ToList();

            return sales;
        }

        private List<Sales> orderByItem(List<Sales> sales)
        {
            return sales.OrderBy(i=>i.Item).ToList();
        }

        private List<Sales> filterPerItem(List<Sales> sales, string filter)
        {
            return (from s in sales where s.Item.name.Contains(filter) select s).ToList();
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