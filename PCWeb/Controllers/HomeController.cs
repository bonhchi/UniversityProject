using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Models;
using System.Diagnostics;
using System.Linq;

namespace PCWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext dataContext;
        public HomeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var item = dataContext.Products.ToList();
            var itemLaptop = dataContext.Products.Where(p => p.CategoryId == 1).ToList();
            var itemPC = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
            var laptopCategory = dataContext.LaptopCategories.Select(p => p).ToList();
            var pcCategory = dataContext.PCCategories.Select(p => p).ToList();
            ViewBag.LaptopCategory = laptopCategory;
            ViewBag.PCCategory = pcCategory;
            ViewBag.PC = itemPC;
            ViewBag.Laptop = itemLaptop;
            return View(item);
        }
        public IActionResult Product()
        {
            var item = dataContext.Products.Select(p => p).ToList();
            return View(item);
        }
        public IActionResult Search(string search)
        {
            var query = dataContext.Products.Where(p => p.ProductName.Contains(search)).ToList();
            if (!string.IsNullOrEmpty(search))
                return View(query);
            return RedirectToAction("Index");
        }
        public IActionResult Intro()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if(ModelState.IsValid)
            {
                Contact newContact = new Contact
                {
                    ContactAddress = contact.ContactAddress,
                    ContactEmail = contact.ContactEmail,
                    ContactName = contact.ContactName,
                    ContactNote = contact.ContactNote,
                    ContactPhone = contact.ContactPhone
                };
                dataContext.Contacts.Add(newContact);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
                return View(contact);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
