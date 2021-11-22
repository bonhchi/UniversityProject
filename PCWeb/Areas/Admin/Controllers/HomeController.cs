using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Models.Name;
using PCWeb.Models.Source;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class HomeController : Controller
    {
        private readonly DataContext dataContext;
        public HomeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var product = dataContext.Products.Select(p => p).ToList();
            var laptop = dataContext.Laptops.Select(p => p).ToList();
            var pc = dataContext.PCs.Select(p => p).ToList();
            var account = dataContext.Users.Select(p => p).ToList();
            List<Product> productLaptop = new List<Product>();
            List<Product> productPC = new List<Product>();
            List<LaptopCategory> laptopCategories = new List<LaptopCategory>();
            List<PCCategory> pcCategories = new List<PCCategory>();
            foreach (var item in laptop)
            {
                Product newLaptop = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                productLaptop.Add(newLaptop);
                LaptopCategory laptopCategory = dataContext.LaptopCategories.FirstOrDefault(p => p.LaptopCategoryId == item.LaptopCategoryId);
                laptopCategories.Add(laptopCategory);
            }
            foreach (var item in pc)
            {
                Product newpc = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                productPC.Add(newpc);
                PCCategory pcCategory = dataContext.PCCategories.FirstOrDefault(p => p.PCCategoryId == item.PCCategoryId);
                pcCategories.Add(pcCategory);
            }
            ViewBag.Product = product.Count();
            ViewBag.Laptop = laptop.Count();
            ViewBag.PC = pc.Count();
            ViewBag.Account = account.Count();
            ViewBag.ListLaptop = laptop;
            ViewBag.ListPC = pc;
            return View();
        }
    }
}
