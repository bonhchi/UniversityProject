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
    public class GPUController : Controller
    {
        private readonly DataContext dataContext;
        public GPUController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var item = dataContext.Graphics.ToList();
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
            Graphic graphic = new Graphic();
            var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 2).ToList();
            var gpuList = dataContext.Graphics.ToList();
            List<PCComponent> itemSelected = product;
            foreach (var item in gpuList)
            {
                PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
            return View(graphic);
        }
        [HttpPost]
        public IActionResult Add(Graphic graphic)
        {
            if (ModelState.IsValid)
            {
                Graphic newGPU = new Graphic
                {
                    GraphicBus = graphic.GraphicBus,
                    GraphicConnector = graphic.GraphicConnector,
                    GraphicCooling = graphic.GraphicCooling,
                    GraphicCore = graphic.GraphicCore,
                    GraphicDimension = graphic.GraphicDimension,
                    GraphicGPU = graphic.GraphicGPU,
                    GraphicGPUBoost = graphic.GraphicGPUBoost,
                    GraphicLED = graphic.GraphicLED,
                    GraphicMaxMonitor = graphic.GraphicMaxMonitor,
                    GraphicMemFrequency = graphic.GraphicMemFrequency,
                    GraphicMemory = graphic.GraphicMemory,
                    GraphicMinPower = graphic.GraphicMinPower,
                    GraphicPCI = graphic.GraphicPCI,
                    GraphicPort = graphic.GraphicPort,
                    GraphicPower = graphic.GraphicPower,
                    GraphicResolution = graphic.GraphicResolution,
                    GraphicSeries = graphic.GraphicSeries,
                    PCComponentId = graphic.PCComponentId
                };
                dataContext.Graphics.Add(newGPU);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "GPU");
            }
            else
            {
                var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 2).ToList();
                var gpuList = dataContext.Graphics.ToList();
                List<PCComponent> itemSelected = product;
                foreach (var item in gpuList)
                {
                    PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                    itemSelected.Remove(query);
                }
                ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
                return View(graphic);
            }
        }
        public IActionResult Edit(int id)
        {
            Graphic oldGPU = dataContext.Graphics.FirstOrDefault(p => p.GraphicId == id);
            return View(oldGPU);
        }
        [HttpPost]
        public IActionResult Edit(int id, Graphic graphic)
        {
            if (ModelState.IsValid)
            {
                Graphic oldGPU = dataContext.Graphics.FirstOrDefault(p => p.GraphicId == id);
                oldGPU.GraphicBus = graphic.GraphicBus;
                oldGPU.GraphicConnector = graphic.GraphicConnector;
                oldGPU.GraphicCooling = graphic.GraphicCooling;
                oldGPU.GraphicCore = graphic.GraphicCore;
                oldGPU.GraphicDimension = graphic.GraphicDimension;
                oldGPU.GraphicGPU = graphic.GraphicGPU;
                oldGPU.GraphicGPUBoost = graphic.GraphicGPUBoost;
                oldGPU.GraphicLED = graphic.GraphicLED;
                oldGPU.GraphicMaxMonitor = graphic.GraphicMaxMonitor;
                oldGPU.GraphicMemFrequency = graphic.GraphicMemFrequency;
                oldGPU.GraphicMemory = graphic.GraphicMemory;
                oldGPU.GraphicMinPower = graphic.GraphicMinPower;
                oldGPU.GraphicPCI = graphic.GraphicPCI;
                oldGPU.GraphicPort = graphic.GraphicPort;
                oldGPU.GraphicPower = graphic.GraphicPower;
                oldGPU.GraphicResolution = graphic.GraphicResolution;
                oldGPU.GraphicSeries = graphic.GraphicSeries;
                oldGPU.PCComponentId = graphic.PCComponentId;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "GPU");
            }
            return View(graphic);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Graphic oldGPU = dataContext.Graphics.FirstOrDefault(p => p.GraphicId == id);
            return View(oldGPU);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Graphic graphic = dataContext.Graphics.FirstOrDefault(p => p.GraphicId == id);
            dataContext.Graphics.Remove(graphic);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "GPU");
        }
    }
}
