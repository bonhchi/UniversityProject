using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCWeb.Data;
using PCWeb.Models;
using PCWeb.Models.Source;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class ProductController : Controller
    {
        private readonly DataContext dataContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            this.dataContext = dataContext;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string search,string name = null)
        {
            var query = dataContext.Products.Where(s => s.Category.CategoryName != null).ToList();
            List<Category> categories = new List<Category>();
            List<Brand> brands = new List<Brand>();
            foreach (var item in query)
            {
                Category category = dataContext.Categories.FirstOrDefault(p => p.CategoryId == item.CategoryId);
                categories.Add(category);
                Brand brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == item.BrandId);
                brands.Add(brand);
            }
            var categoryList = dataContext.Categories.Select(p => p).ToList();
            ViewBag.Selected = categoryList;
            var queryName = dataContext.Products.Where(s => s.ProductName.Contains(search)).ToList();
            if (!string.IsNullOrEmpty(search) && name == "all")
            {
                return View(queryName);
            }
            else if (name != null && !string.IsNullOrEmpty(search))
            {
                var queryNameInCategory = dataContext.Products.Where(s => s.Category.CategoryName == name && s.ProductName.Contains(search)).ToList();
                return View(queryNameInCategory);
            }
            else if (name == null && string.IsNullOrEmpty(search))
                return View(query);
            else if (string.IsNullOrEmpty(search) && name != "all")
            {
                var queryCategory = dataContext.Products.Where(s => s.Category.CategoryName == name).ToList();
                return View(queryCategory);
            }
            return View(query);
        }
        [HttpGet]
        public IActionResult Add()
        {
            Product product = new Product();
            var item = dataContext.Categories.Select(p => p).ToList();
            ViewBag.Category = item;
            ViewBag.SelectedCategory = new SelectList(item, "CategoryId", "CategoryName");
            var brand = dataContext.Brands.Select(p => p).ToList();
            ViewBag.Brand = brand;
            ViewBag.SelectedBrand = new SelectList(brand, "BrandId", "BrandName");
            return View(product);
        }
        [HttpPost]
        public IActionResult Add(Product product, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                Product newProduct = new Product();
                if (photo == null || photo.Length == 0)
                {
                    newProduct.ProductImage = "strixg.jpg";
                }
                else
                {
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(path, photo.FileName.ToString());
                    using (var fileStream = new FileStream(filePath,FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    newProduct.ProductImage = photo.FileName;
                }
                newProduct.BrandId = product.BrandId;
                newProduct.ProductCode = product.ProductCode;
                newProduct.ProductName = product.ProductName;
                newProduct.ProductPrice = product.ProductPrice;
                newProduct.ProductSeries = product.ProductSeries;
                newProduct.ProductWarranty = product.ProductWarranty;
                newProduct.ProductDescription = product.ProductDescription;
                newProduct.DayCreate = DateTime.Now;
                newProduct.ProductQuantity = product.ProductQuantity;
                newProduct.CategoryId = product.CategoryId;
                newProduct.ProductPackage = product.ProductPackage;
                newProduct.ProductPriceReality = product.ProductPrice;
                dataContext.Products.Add(newProduct);
                dataContext.SaveChanges();
                Revenue newRevenue = new Revenue()
                {
                    ProductCode = newProduct.ProductCode,
                    ProductImage = newProduct.ProductImage,
                    ProductPrice = newProduct.ProductPrice,
                    ProductName = newProduct.ProductName,
                    ProductSeries = newProduct.ProductSeries,
                    ProductId = newProduct.ProductId,
                    DayCreate = newProduct.DayCreate
                };
                dataContext.Revenues.Add(newRevenue);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var item = dataContext.Categories.Select(p => p).ToList();
                ViewBag.Category = item;
                ViewBag.Selected = new SelectList(item, "CategoryId", "CategoryName");
                return View(product);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product oldProduct = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            var item = dataContext.Categories.Select(p => p).ToList();
            ViewBag.Category = item;
            ViewBag.SelectedCategory = new SelectList(item, "CategoryId", "CategoryName");
            var brand = dataContext.Brands.Select(p => p).ToList();
            ViewBag.Brand = brand;
            ViewBag.SelectedBrand = new SelectList(brand, "BrandId", "BrandName");
            return View(oldProduct);
        }
        [HttpPost]
        public IActionResult Edit(int id, Product product, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                Product oldProduct = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
                if (photo == null || photo.Length == 0)
                {
                    product.ProductImage = oldProduct.ProductImage;
                }
                else
                {
                    var path = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(path, photo.FileName.ToString());
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                    product.ProductImage = photo.FileName;
                }
                oldProduct.ProductCode = product.ProductCode;
                oldProduct.ProductName = product.ProductName;
                oldProduct.BrandId = product.BrandId;
                oldProduct.ProductPrice = product.ProductPrice;
                oldProduct.ProductDescription = product.ProductDescription;
                oldProduct.ProductImage = product.ProductImage;
                oldProduct.ProductPrice = product.ProductPrice;
                oldProduct.ProductSeries = product.ProductSeries;
                oldProduct.ProductWarranty = product.ProductWarranty;
                oldProduct.ProductQuantity = product.ProductQuantity;
                oldProduct.CategoryId = product.CategoryId;
                oldProduct.ProductPackage = product.ProductPackage;
                oldProduct.DayCreate = DateTime.Now;
                oldProduct.ProductPriceReality = product.ProductPrice;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                var item = dataContext.Categories.Select(p => p).ToList();
                ViewBag.Category = item;
                ViewBag.Selected = new SelectList(item, "CategoryId", "CategoryName");
                var brand = dataContext.Brands.Select(p => p).ToList();
                ViewBag.Brand = brand;
                ViewBag.SelectedBrand = new SelectList(brand, "BrandId", "BrandName");
                return View(product);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product oldProduct = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            return View(oldProduct);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            Revenue revenue = dataContext.Revenues.FirstOrDefault(p => p.ProductId == id);
            List<OrderDetail> orderDetails = dataContext.OrderDetails.Where(p => p.ProductId == id).ToList();
            DeleteOrder(orderDetails);
            revenue.DateExpired = DateTime.Now;
            dataContext.SaveChanges();
            //string deleteImage = Path.Combine(Path.Combine(webHostEnvironment.WebRootPath, "images"), product.ProductImage);
            ////trùng hình là xóa hết
            //if (System.IO.File.Exists(deleteImage))
            //{
            //    System.IO.File.Delete(deleteImage);
            //}
            dataContext.Products.Remove(product);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Product");
        }
        public IActionResult Detail(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }    
            Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            var category = dataContext.Categories.FirstOrDefault(p => p.CategoryId == product.CategoryId);
            var brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == product.BrandId);
            ViewBag.Category = category.CategoryName;
            ViewBag.Brand = brand.BrandName;
            return View(product);
        }
        private void DeleteOrder(List<OrderDetail> orderDetails)
        {
            for (int i = 0; i < orderDetails.Count; i++)
            {
                if (orderDetails[i] == null)
                    break;
                for (int j = i + 1; j < orderDetails.Count; j++)
                {
                    if (orderDetails[i].ProductId == orderDetails[j].ProductId)
                    {
                        dataContext.OrderDetails.Remove(orderDetails[j]);
                        dataContext.SaveChanges();
                        orderDetails.RemoveAt(j);
                        break;
                    }
                }
            }
        }
    }
}
