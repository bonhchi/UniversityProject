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
    public class MainboardController : Controller
    {
        private readonly DataContext dataContext;

        public MainboardController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var item = dataContext.CPUs.ToList();
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
            CPU cpu = new CPU();
            var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 1).ToList();
            var cpuList = dataContext.CPUs.ToList();
            List<PCComponent> itemSelected = product;
            foreach (var item in cpuList)
            {
                PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
            return View(cpu);
        }
        [HttpPost]
        public IActionResult Add(CPU cpu)
        {
            if (ModelState.IsValid)
            {
                CPU newCPU = new CPU
                {
                    CPUCache = cpu.CPUCache,
                    CPUCore = cpu.CPUCore,
                    CPUCode = cpu.CPUCode,
                    CPUModel = cpu.CPUModel,
                    CPUPower = cpu.CPUPower,
                    CPUProcess = cpu.CPUProcess,
                    CPUSocket = cpu.CPUSocket,
                    CPUSpeed = cpu.CPUSpeed,
                    CPUThread = cpu.CPUThread,
                    CPUTurbo = cpu.CPUTurbo,
                    PCComponentId = cpu.PCComponentId,
                };

                dataContext.CPUs.Add(newCPU);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "CPU");
            }
            else
            {
                var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 1).ToList();
                var cpuList = dataContext.CPUs.ToList();
                List<PCComponent> itemSelected = product;
                foreach (var item in cpuList)
                {
                    PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                    itemSelected.Remove(query);
                }
                ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
                return View(cpu);
            }
        }
        public IActionResult Edit(int id)
        {
            CPU oldCPU = dataContext.CPUs.FirstOrDefault(p => p.CPUId == id);
            return View(oldCPU);
        }
        [HttpPost]
        public IActionResult Edit(int id, CPU cpu)
        {
            if (ModelState.IsValid)
            {
                CPU oldCPU = dataContext.CPUs.FirstOrDefault(p => p.CPUId == id);
                oldCPU.CPUCache = cpu.CPUCache;
                oldCPU.CPUCode = cpu.CPUCode;
                oldCPU.CPUCore = cpu.CPUCore;
                oldCPU.CPUModel = cpu.CPUModel;
                oldCPU.CPUPower = cpu.CPUPower;
                oldCPU.CPUProcess = cpu.CPUProcess;
                oldCPU.CPUSocket = cpu.CPUSocket;
                oldCPU.CPUSpeed = cpu.CPUSpeed;
                oldCPU.CPUThread = cpu.CPUThread;
                oldCPU.CPUTurbo = cpu.CPUTurbo;
                oldCPU.PCComponentId = cpu.PCComponentId;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "CPU");
            }
            return View(cpu);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            CPU oldCPU = dataContext.CPUs.FirstOrDefault(p => p.CPUId == id);
            return View(oldCPU);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            CPU cpu = dataContext.CPUs.FirstOrDefault(p => p.CPUId == id);
            dataContext.CPUs.Remove(cpu);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "CPU");
        }
    }
}
