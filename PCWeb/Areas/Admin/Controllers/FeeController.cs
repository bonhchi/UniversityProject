using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Models;
using System.Linq;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class FeeController : Controller
    {
        private readonly DataContext dataContext;
        public FeeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var fee = dataContext.Fees.ToList();
            return View(fee);
        }
        [HttpGet]
        public IActionResult Add()
        {
            Fee fee = new Fee(); 
            return View(fee);
        }
        [HttpPost]
        public IActionResult Add(Fee fee)
        {
            if (ModelState.IsValid)
            {
                Fee newFee = new Fee
                {
                    FeeName = fee.FeeName,
                    FeeAmount = fee.FeeAmount,
                    FeeUnit = fee.FeeUnit
                };
                dataContext.Fees.Add(newFee);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Fee");
            }
            else
                return View(fee);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Fee fee = dataContext.Fees.FirstOrDefault(p => p.FeeId == id);
            return View(fee);
        }
        [HttpPost]
        public IActionResult Edit(int id, Fee fee)
        {
            if (ModelState.IsValid)
            {
                Fee oldFee = dataContext.Fees.FirstOrDefault(p => p.FeeId == id);
                oldFee.FeeAmount = fee.FeeAmount;
                oldFee.FeeName = fee.FeeName;
                oldFee.FeeUnit = fee.FeeUnit;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "Fee");
            }
            else
                return View(fee);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Fee fee = dataContext.Fees.FirstOrDefault(p => p.FeeId == id);
            return View(fee);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            Fee fee = dataContext.Fees.FirstOrDefault(p => p.FeeId == id);
            dataContext.Fees.Remove(fee);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Fee");
        }

    }
}
