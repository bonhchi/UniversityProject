using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCWeb.Data;
using PCWeb.Models.Name;
using PCWeb.Models.Root;
using PCWeb.Models.Source;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class LaptopController : Controller
    {
        private readonly DataContext dataContext;
        public LaptopController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index(string search, string name = null)
        {
            var query = dataContext.Laptops.Where(p => p.LaptopCategory.LaptopCategoryName != null).ToList();
            List<LaptopCategory> laptopCategories = new List<LaptopCategory>();
            List<Product> laptop = new List<Product>();
            foreach (var item in query)
            {
                LaptopCategory laptopCategory = dataContext.LaptopCategories.FirstOrDefault(p => p.LaptopCategoryId == item.LaptopCategoryId);
                laptopCategories.Add(laptopCategory);
                Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                laptop.Add(product);
            }
            var categoryList = dataContext.LaptopCategories.ToList();
            ViewBag.Category = categoryList;
            var queryName = dataContext.Laptops.Where(s => s.Product.ProductName.Contains(search)).ToList();
            if (!string.IsNullOrEmpty(search) && name == "all")
            {
                return View(queryName);
            }
            else if (name != null && !string.IsNullOrEmpty(search))
            {
                var queryNameInCategory = dataContext.Laptops.Where(s => s.LaptopCategory.LaptopCategoryName == name && s.Product.ProductName.Contains(search)).ToList();
                return View(queryNameInCategory);
            }
            else if (name == null && string.IsNullOrEmpty(search))
                return View(query);
            else if (string.IsNullOrEmpty(search) && name != "all")
            {
                var queryCategory = dataContext.Laptops.Where(s => s.LaptopCategory.LaptopCategoryName == name).ToList();
                return View(queryCategory);
            }
            
            return View(query);
        }
        [HttpGet]
        public IActionResult Add()
        {
            Laptop laptop = new Laptop();
            var item = dataContext.Products.Where(p => p.CategoryId == 1).ToList();
            var itemLaptop = dataContext.Laptops.ToList();
            List<Product> itemSelected = item;
            foreach(var item2 in itemLaptop)
            {
                Product query = dataContext.Products.FirstOrDefault(p => p.ProductId == item2.ProductId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "ProductId", "ProductName");
            var category = dataContext.LaptopCategories.ToList();
            ViewBag.Category = new SelectList(category, "LaptopCategoryId", "LaptopCategoryName");
            return View(laptop);
        }
        [HttpPost]
        public IActionResult Add(Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                Laptop newLaptop = new Laptop
                {
                    LaptopBattery = laptop.LaptopBattery,
                    LaptopCPU = laptop.LaptopCPU,
                    LaptopConnectivity = laptop.LaptopConnectivity,
                    LaptopDimension = laptop.LaptopDimension,
                    LaptopGPU = laptop.LaptopGPU,
                    LaptopKeyboard = laptop.LaptopKeyboard,
                    LaptopWebcam = laptop.LaptopWebcam,
                    LaptopScreen = laptop.LaptopScreen,
                    LaptopOS = laptop.LaptopOS,
                    LaptopRAM = laptop.LaptopRAM,
                    LaptopSeries = laptop.LaptopSeries,
                    LaptopStorage = laptop.LaptopStorage,
                    LaptopPort = laptop.LaptopPort,
                    LaptopWeight = laptop.LaptopWeight,
                    ProductId = laptop.ProductId,
                    LaptopColor = laptop.LaptopColor,
                    LaptopCategoryId = laptop.LaptopCategoryId,
                };
                dataContext.Laptops.Add(newLaptop);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Laptop");
            }
            else
            {
                var item = dataContext.Products.Where(p => p.CategoryId == 1).ToList();
                ViewBag.Selected = new SelectList(item, "ProductId", "ProductName");
                var category = dataContext.LaptopCategories.ToList();
                ViewBag.Category = new SelectList(category, "LaptopCategoryId", "LaptopCategoryName");
                return View(laptop);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Laptop oldLaptop = dataContext.Laptops.FirstOrDefault(p => p.LaptopId == id);
            var category = dataContext.LaptopCategories.ToList();
            ViewBag.Category = new SelectList(category, "LaptopCategoryId", "LaptopCategoryName");
            return View(oldLaptop);
        }
        [HttpPost]
        public IActionResult Edit(int id, Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                Laptop oldLaptop = dataContext.Laptops.FirstOrDefault(p => p.LaptopId == id);
                oldLaptop.LaptopBattery = laptop.LaptopBattery;
                oldLaptop.LaptopConnectivity = laptop.LaptopConnectivity;
                oldLaptop.LaptopCPU = laptop.LaptopCPU;
                oldLaptop.LaptopDimension = laptop.LaptopDimension;
                oldLaptop.LaptopPort = laptop.LaptopPort;
                oldLaptop.LaptopGPU = laptop.LaptopGPU;
                oldLaptop.LaptopKeyboard = laptop.LaptopKeyboard;
                oldLaptop.LaptopScreen = laptop.LaptopScreen;
                oldLaptop.LaptopOS = laptop.LaptopOS;
                oldLaptop.LaptopRAM = laptop.LaptopRAM;
                oldLaptop.LaptopSeries = laptop.LaptopSeries;
                oldLaptop.LaptopStorage = laptop.LaptopStorage;
                oldLaptop.LaptopColor = laptop.LaptopColor;
                oldLaptop.LaptopWeight = laptop.LaptopWeight;
                oldLaptop.LaptopWebcam = laptop.LaptopWebcam;
                oldLaptop.LaptopCategoryId = laptop.LaptopCategoryId;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "Laptop");
            }
            else
            {
                var item = dataContext.Products.Where(p => p.CategoryId == 1).ToList();
                ViewBag.Selected = new SelectList(item, "ProductId", "ProductName");
                var category = dataContext.LaptopCategories.ToList();
                ViewBag.Category = new SelectList(category, "LaptopCategoryId", "LaptopCategoryName");
                return View(laptop);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Laptop oldLaptop = dataContext.Laptops.FirstOrDefault(p => p.LaptopId == id);
            return View(oldLaptop);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Laptop laptop = dataContext.Laptops.FirstOrDefault(p => p.LaptopId == id);
            dataContext.Laptops.Remove(laptop);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Laptop");
        }
        public IActionResult Detail(int id)
        {
            Laptop laptop = dataContext.Laptops.FirstOrDefault(p => p.LaptopId == id);
            Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == laptop.ProductId);
            LaptopCategory laptopCategory = dataContext.LaptopCategories.FirstOrDefault(p => p.LaptopCategoryId == laptop.LaptopCategoryId);
            Brand brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == product.BrandId);
            ViewBag.product = product.ProductName;
            ViewBag.brand = brand.BrandName;
            ViewBag.category = laptopCategory.LaptopCategoryName;
            return View(laptop);
        }
    }
}
