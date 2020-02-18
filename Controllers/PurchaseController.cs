using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;

using belajarRazor.Models;
using belajarRazor.Data;
using System.Net.Http.Headers;

namespace belajarRazor.Controllers
{
    public class PurchaseController : Controller
    {
        private AppDbContex appDbContex;

        public PurchaseController(AppDbContex appDbContex)
        {
            this.appDbContex = appDbContex;
        }

        [Authorize]
        public IActionResult Index()
        {
            int userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var cartToProcess = (from i in appDbContex.Carts where i.userID == userId select i).FirstOrDefault();

            return View(cartToProcess);
        }

        [Authorize]
        public async Task<IActionResult> ProcessOrder(IFormCollection prc)
        {
            int userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var cartToProcess = (from i in appDbContex.Carts where i.userID == userId select i).FirstOrDefault();
            // var cartToProcess = appDbContex.Carts.SingleOrDefault(u=>u.userID == userId);

            var prcToProcess = new Purchases
            {
                User = appDbContex.User.Find(userId),
                Address = prc["address"] + "," + prc["postal_code"] + "," + prc["phone"],
                courir = getCourir(prc["shipment"]),
                PaymentMethod = getPayment(prc["payment"]),
                ItemsDetail = cartToProcess
            };

            appDbContex.Purchases.Add(prcToProcess);
            appDbContex.SaveChanges();

            string postBody = generatePostBody(prcToProcess);
            Console.WriteLine(postBody);
            string token = "SB-Mid-server-HGIgu1G5Ny6GYizsGIbSm1uH:";
            string apiUrl = "https://api.sandbox.midtrans.com/v2/charge";
            var response = await ReqObj(apiUrl, HttpMethod.Post, postBody, token);
            
            var transaction = JsonConvert.DeserializeObject<TransactionDetails>(response);
            if(transaction.status_code == "201")
            {
                Console.WriteLine(response);
                var tempPrc = appDbContex.Purchases.Find(prcToProcess.Id);
                tempPrc.TransactionsDetail = transaction;
                
                appDbContex.Carts.Remove(cartToProcess);

                appDbContex.SaveChanges();
            }
            
            else
            {
                appDbContex.Purchases.Remove(prcToProcess);
                appDbContex.SaveChanges();
                return Ok(transaction);
            }

            return RedirectToAction("TransactionDetail", "Purchase");
        }

        private string getCourir(string code)
        {
            switch (code)
            {
                case "1":
                return "JNE-REG";

                case "2":
                return "JNE-YES";

                case "3":
                return "JNT-YES";

                case "4":
                return "POS-Kilat";

                case "5":
                return "COD";
            }

            return "NULL";
        }

        private string getPayment(string pyt)
        {
            switch (pyt)
            {
                case "1":
                return "bni";

                case "2":
                return "bca";

                case "3":
                return "mandiri";

                case "4":
                return "go-pay";

                default:
                return "null";
            }
        }

        private string generatePostBody(Purchases prc)
        {
            if(prc.PaymentMethod == "bni")
            {
                var transaction = new
                {
                    payment_type = "bank_transfer",
                    transaction_details = new
                    {
                        order_id = prc.User.id.ToString() + "-ORD-" +  prc.Id.ToString(),
                        gross_amount = prc.ItemsDetail.totalPrice,
                    },

                    bank_transfer = new {
                        bank = prc.PaymentMethod
                    },
                };

                return JsonConvert.SerializeObject(transaction);
            }

            else if(prc.PaymentMethod == "bca")
            {
                var transaction = new
                {
                    payment_type = "bank_transfer",
                    transaction_details = new
                    {
                        order_id = prc.User.id.ToString() + "-ORD-" +  prc.Id.ToString(),
                        gross_amount = prc.ItemsDetail.totalPrice,
                    },

                    bank_transfer = new {
                        bank = prc.PaymentMethod
                    },
                };

                return JsonConvert.SerializeObject(transaction);
            }

            else if(prc.PaymentMethod == "mandiri")
            {
                var transaction = new
                {
                    payment_type = "echannel",
                    transaction_details = new
                    {
                        order_id = prc.User.id.ToString() + "-ORD-" +  prc.Id.ToString(),
                        gross_amount = prc.ItemsDetail.totalPrice,
                    },
                };

                return JsonConvert.SerializeObject(transaction);
            }

            else if(prc.PaymentMethod == "go-pay")
            {
                var transaction = new
                {
                    payment_type = "gopay",
                    transaction_details = new
                    {
                        order_id = prc.User.id.ToString() + "-ORD-" +  prc.Id.ToString(),
                        gross_amount = prc.ItemsDetail.totalPrice,
                    },
                    gopay = new 
                    {
                        enable_callback = true,
                        callback_url = "https://localhost:5001"
                    }
                };

                return JsonConvert.SerializeObject(transaction);
            }

            return null;
        }

        static async Task<string> ReqObj(string url, HttpMethod methode, string data="", string token="")
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            HttpClient client = new HttpClient(handler);

            if(token != "")
            {
                var byteToken = Encoding.ASCII.GetBytes(token);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteToken));
            }

            var stringCOntent = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            HttpRequestMessage req = new HttpRequestMessage(methode, url);
            req.Content = stringCOntent;
            HttpResponseMessage response = await client.SendAsync(req);
            return await response.Content.ReadAsStringAsync();
        }
    
        private int getAuth()
        {
            if(HttpContext.Session.GetString("JWToken") != null)
                return 1;

            else
                return 0;
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//

        // [Authorize]
        public async Task<IActionResult> TransactionDetail()
        {
            if (getAuth() == 0)
                return View("_LoginAttemp");

            int userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var transactions = from t in appDbContex.Purchases.Include(t=>t.TransactionsDetail)
                               .Include(u=>u.User) where t.User.id == userId select t;
            
            string token = "SB-Mid-server-HGIgu1G5Ny6GYizsGIbSm1uH:";
            string apiUrl = "https://api.sandbox.midtrans.com/v2/";
            foreach(var tr in transactions)
            {
                string orderId = tr.TransactionsDetail.order_id.ToString();
                var status = await ReqObj(apiUrl+orderId+"/status", HttpMethod.Get, "", token);
                var temp = appDbContex.Purchases.Find(tr.Id);
                temp.TransactionsDetail.transaction_status = getTransactionStatus(status);
            }

            appDbContex.SaveChanges();
            ViewBag.auth = getAuth();

            return View("PurchaseDetail", transactions.ToList());
        }

        private string getTransactionStatus(string response)
        {
            var status = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            return status["transaction_status"].ToString();
        }
    }
}