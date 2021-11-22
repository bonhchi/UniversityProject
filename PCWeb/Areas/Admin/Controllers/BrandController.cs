using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Models.Source;
namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class BrandController : Controller
    {
        private readonly DataContext dataContext;
        public BrandController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index(string search)
        {
            var brand = dataContext.Brands.Select(p => p).ToList();
            var query = dataContext.Brands.Where(p => p.BrandName.Contains(search)).ToList();
            if (!string.IsNullOrEmpty(search))
                return View(query);
            return View(brand);
        }
        [HttpGet]
        public IActionResult Add()
        {
            Brand brand = new Brand();
            return View(brand);
        }
        [HttpPost]
        public IActionResult Add(Brand brand)
        {
            if(ModelState.IsValid)
            {
                Brand newBrand = new Brand()
                {
                    BrandName = brand.BrandName
                };
                dataContext.Brands.Add(newBrand);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Brand");
            }
            return View(brand);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Brand brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == id);
            return View(brand);
        }
        [HttpPost]
        public IActionResult Edit(int id, Brand brand)
        {
            if (ModelState.IsValid)
            {
                Brand oldBrand = dataContext.Brands.FirstOrDefault(p => p.BrandId == id);
                oldBrand.BrandName = brand.BrandName;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "Brand");
            }
            return View(brand);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Brand oldBrand = dataContext.Brands.FirstOrDefault(p => p.BrandId == id);
            return View(oldBrand);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Brand brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == id);
            dataContext.Brands.Remove(brand);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }
        public IActionResult Detail(int id)
        {
            Brand brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == id);
            return View(brand);
        }
    }
}
