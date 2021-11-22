using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCWeb.Data;
using PCWeb.Models.Name;
using PCWeb.Models.Source;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class ComponentController : Controller
    {
        private readonly DataContext dataContext;
        public ComponentController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var item = dataContext.PCComponents.ToList();
            List<Product> pc = new List<Product>();
            List<ComponentCategory> componentCategories = new List<ComponentCategory>();
            foreach (var item2 in item)
            {
                ComponentCategory componentCategory = dataContext.ComponentCategories.FirstOrDefault(p => p.ComponentCategoryId == item2.ComponentCategoryId);
                Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item2.ProductId);
                pc.Add(product);
            }
            return View(item);
        }
        [HttpGet]
        public IActionResult Add()
        {
            PCComponent pcComponent = new PCComponent();
            var product = dataContext.Products.Where(p => p.CategoryId == 3).ToList();
            var name = dataContext.PCComponents.ToList();
            List<Product> itemSelected = product;
            foreach (var item in name)
            {
                Product query = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "ProductId", "ProductName");
            var category = dataContext.ComponentCategories.Select(p => p).ToList();
            ViewBag.Category = new SelectList(category, "ComponentCategoryId", "ComponentCategoryName");
            return View(pcComponent);
            
        }
        [HttpPost]
        public IActionResult Add(PCComponent pcComponent)
        {
            if(ModelState.IsValid)
            {
                var query = dataContext.Products.Find(pcComponent.ProductId);
                PCComponent component = new PCComponent
                {
                    ProductId = pcComponent.ProductId,
                    ComponentCategoryId = pcComponent.ComponentCategoryId,
                    PCComponentName = query.ProductName
                };
                dataContext.PCComponents.Add(component);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Component");
            }
            else
            {
                var item = dataContext.Products.Where(p => p.CategoryId == 3).ToList();
                ViewBag.Selected = new SelectList(item, "ProductId", "ProductName");
                var component = dataContext.ComponentCategories.ToList();
                ViewBag.Category = new SelectList(component, "ComponentCategoryId", "ComponentCategoryName");
                return View(pcComponent);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            PCComponent component = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == id);
            return View(component);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            PCComponent component = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == id);
            dataContext.PCComponents.Remove(component);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Component");
        }
        public IActionResult Category()
        {
            return View();
        }
    }
}
