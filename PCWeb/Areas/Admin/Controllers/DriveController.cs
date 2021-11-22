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
    public class DriveController : Controller
    {
        private readonly DataContext dataContext;
        public DriveController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var item = dataContext.Drives.ToList();
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
            Drive drive = new Drive();
            var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 3).ToList();
            var driveList = dataContext.Drives.ToList();
            List<PCComponent> itemSelected = product;
            foreach (var item in driveList)
            {
                PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
            return View(drive);
        }
        [HttpPost]
        public IActionResult Add(Drive drive)
        {
            if (ModelState.IsValid)
            {
                Drive newDrive = new Drive
                {
                    DriveCapacity = drive.DriveCapacity,
                    DriveConnectivity = drive.DriveConnectivity,
                    DriveRead = drive.DriveRead,
                    DriveSize = drive.DriveSize,
                    DriveWrite = drive.DriveWrite,
                    DriveSpec = drive.DriveSpec,
                    PCComponentId = drive.PCComponentId
                };

                dataContext.Drives.Add(newDrive);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Drive");
            }
            else
            {
                var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 3).ToList();
                var cpuList = dataContext.Drives.ToList();
                List<PCComponent> itemSelected = product;
                foreach (var item in cpuList)
                {
                    PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                    itemSelected.Remove(query);
                }
                ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
                return View(drive);
            }
        }
        public IActionResult Edit(int id)
        {
            Drive oldDrive = dataContext.Drives.FirstOrDefault(p => p.DriveId == id);
            return View(oldDrive);
        }
        [HttpPost]
        public IActionResult Edit(int id, Drive drive)
        {
            if (ModelState.IsValid)
            {
                Drive oldDrive = dataContext.Drives.FirstOrDefault(p => p.DriveId == id);
                oldDrive.DriveCapacity = drive.DriveCapacity;
                oldDrive.DriveConnectivity = drive.DriveConnectivity;
                oldDrive.DriveRead = drive.DriveRead;
                oldDrive.DriveSize = drive.DriveSize;
                oldDrive.DriveSpec = drive.DriveSpec;
                oldDrive.DriveWrite = drive.DriveWrite;
                oldDrive.PCComponentId = drive.PCComponentId;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "Drive");
            }
            return View(drive);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Drive oldDrive = dataContext.Drives.FirstOrDefault(p => p.DriveId == id);
            return View(oldDrive);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Drive drive = dataContext.Drives.FirstOrDefault(p => p.DriveId == id);
            dataContext.Drives.Remove(drive);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Drive");
        }
    }
}
