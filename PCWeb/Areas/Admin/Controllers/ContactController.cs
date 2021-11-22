using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Models;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class ContactController : Controller
    {
        private readonly DataContext dataContext;
        public ContactController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var contactList = dataContext.Contacts.Select(p => p).ToList();
            return View(contactList);
        }
        public IActionResult Detail(int id)
        {
            var contact = dataContext.Contacts.FirstOrDefault(p => p.ContactId == id);
            return View(contact);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Contact oldContact = dataContext.Contacts.FirstOrDefault(p => p.ContactId == id);
            return View(oldContact);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Contact contact = dataContext.Contacts.FirstOrDefault(p => p.ContactId == id);
            dataContext.Contacts.Remove(contact);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Contact");
        }
    }
}
