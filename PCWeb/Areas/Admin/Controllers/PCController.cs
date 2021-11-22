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
    public class PCController : Controller
    {
        private readonly DataContext dataContext;
        public PCController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index(string search, string name = null)
        {
            var query = dataContext.PCs.Select(p => p).ToList();
            List<PCCategory> pcCategories = new List<PCCategory>();
            List<Product> pc = new List<Product>();
            foreach (var item in query)
            {
                PCCategory pcCategory = dataContext.PCCategories.FirstOrDefault(p => p.PCCategoryId == item.PCCategoryId);
                pcCategories.Add(pcCategory);
                Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                pc.Add(product);
            }
            var categoryList = dataContext.PCCategories.Select(p => p).ToList();
            ViewBag.Category = categoryList;
            var queryName = dataContext.PCs.Where(s => s.Product.ProductName.Contains(search)).ToList();
            if (!string.IsNullOrEmpty(search) && name == "all")
            {
                return View(queryName);
            }
            else if (name != null && !string.IsNullOrEmpty(search))
            {
                var queryNameInCategory = dataContext.PCs.Where(s => s.PCCategory.PCCategoryName == name && s.Product.ProductName.Contains(search)).ToList();
                return View(queryNameInCategory);
            }
            else if (name == null && string.IsNullOrEmpty(search))
                return View(query);
            else if (string.IsNullOrEmpty(search) && name != "all")
            {
                var queryCategory = dataContext.PCs.Where(s => s.PCCategory.PCCategoryName == name).ToList();
                return View(queryCategory);
            }
            return View(query);
        }
        public IActionResult Add()
        {
            PC pc = new PC();
            var item = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
            var itemPC = dataContext.PCs.Select(p => p).ToList();
            List<Product> itemSelected = item;
            foreach (var item2 in itemPC)
            {
                Product query = dataContext.Products.FirstOrDefault(p => p.ProductId == item2.ProductId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "ProductId", "ProductName");
            var category = dataContext.PCCategories.Select(p => p).ToList();
            ViewBag.Category = new SelectList(category, "PCCategoryId", "PCCategoryName");
            return View(pc);
        }
        [HttpPost]
        public IActionResult Add(PC pc)
        {
            if (ModelState.IsValid)
            {
                PC newPC = new PC
                {
                    PCCase = pc.PCCase,
                    PCCooling = pc.PCCooling,
                    PCCpu = pc.PCCpu,
                    PCGpu = pc.PCGpu,
                    PCMainboard = pc.PCMainboard,
                    PCPsu = pc.PCPsu,
                    PCRam = pc.PCRam,
                    PCStorage = pc.PCStorage,
                    ProductId = pc.ProductId,
                    PCCategoryId = pc.PCCategoryId
                };
                dataContext.PCs.Add(newPC);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "PC");
            }
            else
            {
                var item = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
                ViewBag.Selected = new SelectList(item, "ProductId", "ProductName");
                var category = dataContext.PCCategories.Select(p => p).ToList();
                ViewBag.Category = new SelectList(category, "PCCategoryId", "PCCategoryName");
                return View(pc);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            PC oldPC = dataContext.PCs.FirstOrDefault(p => p.PCId == id);
            var category = dataContext.PCCategories.Select(p => p).ToList();
            ViewBag.Category = new SelectList(category, "PCCategoryId", "PCCategoryName");
            return View(oldPC);
        }
        [HttpPost]
        public IActionResult Edit(int id, PC pc)
        {
            if (ModelState.IsValid)
            {
                PC oldPC = dataContext.PCs.FirstOrDefault(p => p.ProductId == id);
                oldPC.PCMainboard = pc.PCMainboard;
                oldPC.PCCpu = pc.PCCpu;
                oldPC.PCCase = pc.PCCase;
                oldPC.PCCooling = pc.PCCooling;
                oldPC.PCGpu = pc.PCGpu;
                oldPC.PCPsu = pc.PCPsu;
                oldPC.PCRam = pc.PCRam;
                oldPC.PCStorage = pc.PCStorage;
                oldPC.PCCategoryId = pc.PCCategoryId;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "PC");
            }
            else
            {
                var item = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
                ViewBag.Selected = new SelectList(item, "ProductId", "ProductName");
                var category = dataContext.PCCategories.Select(p => p).ToList();
                ViewBag.Category = new SelectList(category, "PCCategoryId", "PCCategoryName");
            }
            return View(pc);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            PC oldPC = dataContext.PCs.FirstOrDefault(p => p.PCId == id);
            return View(oldPC);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            PC pc = dataContext.PCs.FirstOrDefault(p => p.PCId == id);
            dataContext.PCs.Remove(pc);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "PC");
        }
        public IActionResult Detail(int id)
        {
            PC pc = dataContext.PCs.FirstOrDefault(p => p.PCId == id);
            Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == pc.ProductId);
            PCCategory pcCategory = dataContext.PCCategories.FirstOrDefault(p => p.PCCategoryId == pc.PCCategoryId);
            Brand brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == product.BrandId);
            ViewBag.product = product.ProductName;
            ViewBag.brand = brand.BrandName;
            ViewBag.category = pcCategory.PCCategoryName;
            return View(pc);
        }
    }
}
