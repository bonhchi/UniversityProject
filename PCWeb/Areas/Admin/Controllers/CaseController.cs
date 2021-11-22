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
    public class CaseController : Controller
    {
        private readonly DataContext dataContext;
        public CaseController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var item = dataContext.Cases.ToList();
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
            Case casePC = new Case();
            var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 8).ToList();
            var caseList = dataContext.Cases.ToList();
            List<PCComponent> itemSelected = product;
            foreach (var item in caseList)
            {
                PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                itemSelected.Remove(query);
            }
            ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
            return View(casePC);
        }
        [HttpPost]
        public IActionResult Add(Case casePC)
        {
            if (ModelState.IsValid)
            {
                Case newCase = new Case
                {
                    CaseColor = casePC.CaseColor,
                    CaseDimension = casePC.CaseDimension,
                    CaseDrive = casePC.CaseDrive,
                    CaseMaterial = casePC.CaseMaterial,
                    CasePort = casePC.CasePort,
                    CaseSpec = casePC.CaseSpec,
                    CaseSupport = casePC.CaseSupport,
                    PCComponentId = casePC.PCComponentId
                };

                dataContext.Cases.Add(newCase);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Case");
            }
            else
            {
                var product = dataContext.PCComponents.Where(p => p.ComponentCategoryId == 8).ToList();
                var caseList = dataContext.Cases.ToList();
                List<PCComponent> itemSelected = product;
                foreach (var item in caseList)
                {
                    PCComponent query = dataContext.PCComponents.FirstOrDefault(p => p.PCComponentId == item.PCComponentId);
                    itemSelected.Remove(query);
                }
                ViewBag.Selected = new SelectList(itemSelected, "PCComponentId", "PCComponentName");
                return View(casePC);
            }
        }
        public IActionResult Edit(int id)
        {
            Case oldCase = dataContext.Cases.FirstOrDefault(p => p.CaseId == id);
            return View(oldCase);
        }
        [HttpPost]
        public IActionResult Edit(int id, Case casePC)
        {
            if (ModelState.IsValid)
            {
                Case oldCase = dataContext.Cases.FirstOrDefault(p => p.CaseId == id);
                oldCase.CaseColor = casePC.CaseColor;
                oldCase.CaseDimension = casePC.CaseDimension;
                oldCase.CaseDrive = casePC.CaseDrive;
                oldCase.CaseMaterial = casePC.CaseMaterial;
                oldCase.CasePort = casePC.CasePort;
                oldCase.CaseSpec = casePC.CaseSpec;
                oldCase.CaseSupport = casePC.CaseSupport;
                oldCase.PCComponentId = casePC.PCComponentId;
                dataContext.SaveChanges();
                ViewBag.Status = 1;
                return RedirectToAction("Index", "Case");
            }
            return View(casePC);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Case oldCase = dataContext.Cases.FirstOrDefault(p => p.CaseId == id);
            return View(oldCase);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Case casePC = dataContext.Cases.FirstOrDefault(p => p.CaseId == id);
            dataContext.Cases.Remove(casePC);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Case");
        }
    }
}
