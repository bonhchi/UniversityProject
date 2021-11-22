using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Models.Name;
using PCWeb.Models.Root;
using PCWeb.Models.Source;
using System.Linq;
namespace PCWeb.Controllers
{
    public class DetailController : Controller
    {
        private readonly DataContext dataContext;
        public DetailController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index(int id)
        {
            Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == id);
            Brand brand = dataContext.Brands.FirstOrDefault(p => p.BrandId == product.BrandId);
            if (product.CategoryId == 1)
            {
                Laptop laptop = dataContext.Laptops.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (laptop == null)
                    ViewBag.Status = 1;
                else
                {
                    ViewBag.Laptop = laptop;
                    LaptopCategory laptopCategory = dataContext.LaptopCategories.FirstOrDefault(p => p.LaptopCategoryId == laptop.LaptopCategoryId);
                    ViewBag.Category = laptopCategory.LaptopCategoryName;
                }
                
            }
            else
            {
                PC pc = dataContext.PCs.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (pc == null)
                    ViewBag.Status = 1;
                else
                {
                    ViewBag.PC = pc;
                    PCCategory pCCategory = dataContext.PCCategories.FirstOrDefault(p => p.PCCategoryId == pc.PCCategoryId);
                    ViewBag.Category = pCCategory.PCCategoryName;
                }
                
            }
            return View(product);
        }
    }
}
