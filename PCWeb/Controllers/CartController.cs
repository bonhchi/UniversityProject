using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BraintreeHttp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PayPal.Core;
using PayPal.v1.Payments;
using PCWeb.Data;
using PCWeb.Helper;
using PCWeb.Models;
using PCWeb.Models.Account;
using PCWeb.Models.Source;

namespace PCWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IConfiguration config;
        private readonly string clientId;
        private readonly string secretKey;
        private const double exchange = 23220;
        private const string currency = "USD";
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public CartController(DataContext dataContext, IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.dataContext = dataContext;
            this.config = config;
            clientId = config["PaypalSettings:ClientId"];
            secretKey = config["PaypalSettings:SecretKey"];
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            if (cart == null)
                return View();
            else
            {
                TestViewBag(cart);
            }
            return View();
        }
        public IActionResult Quantity(int id, int quantity)
        {
            List<OrderDetail> cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            if (index != -1)
                cart[index].Quantity = quantity;
            var query = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (quantity > query.ProductQuantity)
                ViewBag.Status = 1;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart") == null)
            {
                List<OrderDetail> cart = new List<OrderDetail>
                {
                    new OrderDetail { Product = dataContext.Products.FirstOrDefault(p => p.ProductId == id), Quantity = 1}
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderDetail> cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
                int index = IsExist(id);
                if (index != -1)
                    cart[index].Quantity++;
                else
                    cart.Add(new OrderDetail { Product = dataContext.Products.FirstOrDefault(p => p.ProductId == id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            List<OrderDetail> cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private void TestViewBag(List<OrderDetail> cart)
        {
            ViewBag.cart = cart;
            double total = cart.Sum(item => item.Product.ProductPrice * item.Quantity);
            double vat = dataContext.Fees.FirstOrDefault(p => p.FeeId == 2).FeeAmount / 100;
            double vatFee = vat * total;
            double weight = 0;
            foreach (var item in cart)
            {
                weight += Math.Round(item.Product.ProductPackage * item.Quantity , 1);
            }
            double weightCost = weight * dataContext.Fees.FirstOrDefault(p => p.FeeId == 1).FeeAmount;
            ViewBag.VAT = (vat * 100).ToString();
            ViewBag.VATfee = vatFee;
            ViewBag.Weight = weight.ToString();
            ViewBag.WeightCost = weightCost;
            ViewBag.total = total + vatFee + weightCost;
            ViewBag.Point = Math.Round(total / 10000, 0);
        }
        private int IsExist(int id)
        {
            List<OrderDetail> cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductId == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult Confirm()
        {
            ViewBag.Status = TempData["check"];
            if (TempData["check"] == null)
            {
                return View();
            }
            else
            {
                string queryString = TempData["check"].ToString();
                ViewBag.Name = queryString;
                int queryId = int.Parse(queryString);
                var order = dataContext.Orders.FirstOrDefault(p => p.OrderId == queryId);
                List<OrderDetail> cart = dataContext.OrderDetails.Where(p => p.OrderId == order.OrderId).ToList();
                var productList = dataContext.Products.ToList();
                TestViewBag(cart);
                return View();
            }
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            List<OrderDetail> cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            if (cart == null)
                return View();
            else
            {
                
                TestViewBag(cart);
            }
            var order = new Models.Order();
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Models.Order order, string answer)
        {
            double point = 0;
            List<OrderDetail> cart = SessionHelper.GetObjectFromJson<List<OrderDetail>>(HttpContext.Session, "cart");
            if (ModelState.IsValid)
            {
                if(answer == "paypal")
                {
                    var environment = new SandboxEnvironment(clientId, secretKey);
                    var client = new PayPalHttpClient(environment);
                    var itemList = new ItemList()
                    {
                        Items = new List<Item>()
                    };
                    var total = cart.Sum(item => item.Product.ProductPrice * item.Quantity);
                    foreach (var item in cart)
                    {
                        itemList.Items.Add(new Item()
                        {
                            Name = item.Product.ProductName,
                            Currency = currency,
                            Price = Math.Round(item.Product.ProductPrice / exchange, 2).ToString(),
                            Quantity = item.Quantity.ToString(),
                            Sku = "sku",
                            Tax = "0"
                        });

                    }
                    var paypalOrderId = DateTime.Now.Ticks;
                    var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                    var payment = new Payment()
                    {
                        Intent = "sale",
                        Transactions = new List<Transaction>()
                    {
                    new Transaction ()
                    {
                        Amount = new Amount()
                        {
                            Total = Math.Round(total/exchange,2).ToString(),
                            Currency = currency,
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = Math.Round(total/exchange,2).ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                        RedirectUrls = new RedirectUrls()
                        {
                            CancelUrl = $"{hostname}/Cart/Fail",
                            ReturnUrl = $"{hostname}/Cart/Confirm",
                        },
                        Payer = new Payer()
                        {
                            PaymentMethod = "paypal"
                        }
                    };
                    PaymentCreateRequest request = new PaymentCreateRequest();
                    request.RequestBody(payment);
                    try
                    {
                        var response = await client.Execute(request);
                        var statusCode = response.StatusCode;
                        Payment result = response.Result<Payment>();
                        var links = result.Links.GetEnumerator();
                        string paypalRedirectUrl = null;
                        while (links.MoveNext())
                        {
                            LinkDescriptionObject link = links.Current;
                            if (link.Rel.ToLower().Trim().Equals("approval_url"))
                                paypalRedirectUrl = link.Href;
                        }
                        Models.Order orderTemp = new Models.Order
                        {
                            OrderDate = DateTime.Now,
                            Phone = order.Phone,
                            Address = order.Address,
                            Email = order.Email,
                            CusName = order.CusName,
                            Note = order.Note,
                            OrderConditionId = 1,
                            PaymentMethodId = 2,
                            OrderCheckout = "Đã thanh toán"
                        };
                        dataContext.Orders.Add(orderTemp);
                        dataContext.SaveChanges();
                        var query = dataContext.Orders.FirstOrDefault(p => p.OrderId == orderTemp.OrderId);
                        foreach (var item in cart)
                        {
                            dataContext.OrderDetails.Add(new OrderDetail()
                            {
                                OrderId = query.OrderId,
                                ProductId = item.Product.ProductId,
                                Quantity = item.Quantity,
                            });
                            Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.Product.ProductId);
                            product.ProductQuantity -= item.Quantity;
                        }
                        dataContext.SaveChanges();
                        var user = await _userManager.FindByEmailAsync(order.Email);
                        if (user != null)
                        {
                            user.UserPoint += Convert.ToInt32(point);
                            await _userManager.UpdateAsync(user);
                        }
                        List<Product> products = new List<Product>();
                        foreach (var item in cart)
                        {
                            Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.Product.ProductId);
                            products.Add(product);
                            var revenueId = dataContext.Revenues.FirstOrDefault(p => p.ProductId == item.Product.ProductId).RevenueId;
                            dataContext.RevenueDetails.Add(new RevenueDetail()
                            {
                                RevenueId = revenueId,
                                DateIssue = DateTime.Now,
                                Quantity = item.Quantity,
                                PriceReality = item.Product.ProductPrice
                            });
                        }
                        dataContext.SaveChanges();
                        cart.Clear();
                        TempData["check"] = query.OrderId;
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        return Redirect(paypalRedirectUrl);
                    }
                    catch (HttpException httpException)
                    {
                        var statusCode = httpException.StatusCode;
                        var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                        return Redirect("/Cart/Fail");
                    } 
                }
                else
                {
                    Models.Order orderTemp = new Models.Order
                    {
                        OrderDate = DateTime.Now,
                        Phone = order.Phone,
                        Address = order.Address,
                        Email = order.Email,
                        CusName = order.CusName,
                        PaymentMethodId = 1,
                        Note = order.Note,
                        OrderConditionId = 1,
                        OrderCheckout = "Chưa thanh toán"
                    };
                    dataContext.Orders.Add(orderTemp);
                    dataContext.SaveChanges();
                    var query = dataContext.Orders.FirstOrDefault(p => p.OrderId == orderTemp.OrderId);
                    foreach (var item in cart)
                    {
                        dataContext.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = query.OrderId,
                            ProductId = item.Product.ProductId,
                            Quantity = item.Quantity,
                        });
                        Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.Product.ProductId);
                        product.ProductQuantity -= item.Quantity;
                        point += (item.Product.ProductPrice / 10000);
                    }
                    dataContext.SaveChanges();
                    cart.Clear();
                    TempData["check"] = query.OrderId;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                    return RedirectToAction("Confirm", "Cart");
                }
            }
            else
            {
                if (cart != null)
                {
                    TestViewBag(cart);
                }
                else
                    return View();
                return View(order);
            }
        }
        public IActionResult Fail()
        {
            return View();
        }
    }
}
