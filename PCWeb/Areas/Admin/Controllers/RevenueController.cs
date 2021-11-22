using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class RevenueController : Controller
    {
        private readonly DataContext dataContext;
        public RevenueController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var revenueList = dataContext.Revenues.ToList();
            Data(revenueList);
            return View(revenueList);
        }
        public IActionResult Detail(int id)
        {
            Revenue revenue = dataContext.Revenues.FirstOrDefault(p => p.RevenueId == id);
            var revenueList = dataContext.Revenues.Where(p => p.RevenueId == id).ToList();
            List<int> quantity = new List<int>();
            List<double> allTotal = new List<double>();
            List<RevenueDetail> detail = new List<RevenueDetail>();
            for (int i = 0; i < revenueList.Count; i++)
            {
                int count = 0;
                double total = 0;
                var find = dataContext.RevenueDetails.Where(p => p.RevenueId == revenueList[i].RevenueId).ToList();
                if (find.Count > 0)
                {
                    foreach (var item in find)
                    {
                        count += item.Quantity;
                        total += item.PriceReality;
                    }
                    quantity.Add(count);
                    allTotal.Add(total);
                    var findOne = dataContext.RevenueDetails.FirstOrDefault(p => p.RevenueId == revenueList[i].RevenueId);
                    findOne.Quantity = count;
                    detail.Add(findOne);
                }
            }
            string dateCreate = revenue.DayCreate.ToString("dd/MM/yyyy");
            string dateExpired = revenue.DateExpired.ToString("dd/MM/yyyy");
            ViewBag.DayCreate = dateCreate;
            ViewBag.Detail = detail;
            ViewBag.DayExpried = dateExpired;
            ViewBag.Quantity = quantity;
            ViewBag.Total = allTotal;
            return View(revenue);
        }
        [HttpGet]
        public IActionResult Search(string search)
        {
            var revenueList = dataContext.Revenues.ToList();
            if(!string.IsNullOrEmpty(search))
            {
                var query = dataContext.Revenues.Where(p => p.ProductName.Contains(search)).ToList();
                Data(query);
                return View("Index", query);
            }
            Data(revenueList);
            return View("Index", revenueList);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Revenue revenue = dataContext.Revenues.FirstOrDefault(p => p.RevenueId == id);
            return View(revenue);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Revenue revenue = dataContext.Revenues.FirstOrDefault(p => p.RevenueId == id);
            dataContext.Revenues.Remove(revenue);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Revenue");
        }
        private void Data(List<Revenue> revenues)
        {
            List<int> quantity = new List<int>();
            List<double> allTotal = new List<double>();
            for (int i = 0; i < revenues.Count; i++)
            {
                int count = 0;
                double total = 0;
                var find = dataContext.RevenueDetails.Where(p => p.RevenueId == revenues[i].RevenueId).ToList();
                if (find != null)
                {
                    foreach (var item in find)
                    {
                        count += item.Quantity;
                        total += item.Quantity * item.PriceReality;
                    }
                    quantity.Add(count);
                    allTotal.Add(total);
                }
            }
            ViewBag.Quantity = quantity;
            ViewBag.Total = allTotal;
            
        }
    }
}
