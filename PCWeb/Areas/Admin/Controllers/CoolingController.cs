using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PCWeb.Data;
using PCWeb.Models.Name;
using PCWeb.Models.Root;

namespace PCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator, Staff")]
    public class CoolingController : Controller
    {
        private readonly DataContext dataContext;
        public CoolingController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var item = dataContext.Coolings.ToList();
            List<PCComponent> pcComponents = new List<PCComponent>();
            foreach (var item2 in item)
            {
                PCComponent pcComponent = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item2.PCComponentId);
                pcComponents.Add(pcComponent);
            }
            return View(item);
        }
        public IActionResult Add()
        {
            Cooling cooling = new Cooling();
            var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 5).ToList();
            var coolingList = dataContext.Coolings.ToList();
            List<PCComponent> itemSelected = product;
            foreach (var item in coolingList)
            {
                PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
            return View(cooling);
        }
        [HttpPost]
        public IActionResult Add(Cooling cooling)
        {
            if (ModelState.IsValid)
            {
                Cooling newCooling = new Cooling
                {
                    CoolingColor = cooling.CoolingColor,
                    CoolingDimension = cooling.CoolingDimension,
                    CoolingLED = cooling.CoolingLED,
                    CoolingMaterial = cooling.CoolingMaterial,
                    CoolingNoise = cooling.CoolingNoise,
                    CoolingRPM = cooling.CoolingRPM,
                    CoolingSocket = cooling.CoolingSocket,
                    CoolingSpec = cooling.CoolingSpec,
                    CoolingWeight = cooling.CoolingWeight,
                    PCComponentId = cooling.PCComponentId
                };

                dataContext.Coolings.Add(newCooling);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Cooling");
            }
            else
            {
                var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 5).ToList();
                var coolingList = dataContext.Coolings.ToList();
                List<PCComponent> itemSelected = product;
                foreach (var item in coolingList)
                {
                    PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                    itemSelected.Remove(query);
                }
                ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
                return View(cooling);
            }
        }
        public IActionResult Edit(int id)
        {
            Cooling oldCooling = dataContext.Coolings.FirstOrDefault(p => p.CoolingId == id);
            return View(oldCooling);
        }
        [HttpPost]
        public IActionResult Edit(int id, Cooling cooling)
        {
            if (ModelState.IsValid)
            {
                Cooling oldCooling = dataContext.Coolings.FirstOrDefault(p => p.CoolingId == id);
                oldCooling.CoolingColor = cooling.CoolingColor;
                oldCooling.CoolingDimension = cooling.CoolingDimension;
                oldCooling.CoolingLED = cooling.CoolingLED;
                oldCooling.CoolingMaterial = oldCooling.CoolingMaterial;
                oldCooling.CoolingNoise = oldCooling.CoolingNoise;
                oldCooling.CoolingRPM = cooling.CoolingRPM;
                oldCooling.CoolingSocket = cooling.CoolingSocket;
                oldCooling.CoolingSpec = cooling.CoolingSpec;
                oldCooling.CoolingWeight = cooling.CoolingWeight;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "Cooling");
            }
            return View(cooling);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Cooling oldCooling = dataContext.Coolings.FirstOrDefault(p => p.CoolingId == id);
            return View(oldCooling);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Cooling cooling = dataContext.Coolings.FirstOrDefault(p => p.CoolingId == id);
            dataContext.Coolings.Remove(cooling);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Cooling");
        }
    }
}
