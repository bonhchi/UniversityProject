using Microsoft.AspNetCore.Mvc;
using PCWeb.Data;
using PCWeb.Helper;
using PCWeb.Models.Root;
using PCWeb.Models.Source;
using System.Collections.Generic;
using System.Linq;

namespace PCWeb.Controllers
{
    public class PCController : Controller
    {
        private readonly DataContext dataContext;
        private const string KeyCPU = "cpuListPC";
        private const string KeyGPU = "brandListPC";
        private const string KeyPrice = "priceListPC";
        public PCController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public IActionResult Index()
        {
            var itemPC = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
            return View(itemPC);
        }
        public IActionResult Category(int id)
        {
            var itemPC = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
            var pcs = new List<PC>();
            var result = new List<Product>();
            foreach (var item in itemPC)
            {
                PC pc = dataContext.PCs.FirstOrDefault(p => p.ProductId == item.ProductId && p.PCId == id);
                pcs.Add(pc);
            }
            foreach (var item in pcs)
            {
                if (item != null)
                {
                    Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                    result.Add(product);
                }
            }
            ViewBag.PCCategory = dataContext.PCCategories.Find(id).PCCategoryName;
            if (result.Count > 0)
                return View(result.ToList());
            else
                return View();
        }
        public IActionResult FilterCPUMain(string cpu)
        {
            return View("Index", FilterCPU(cpu));
        }
        public IActionResult FilterPriceMain(string price)
        {
            return View("Index", FilterPrice(price));
        }
        public IActionResult FilterGPUMain(string gpu)
        {
            return View("Index", FilterGPU(gpu));
        }
        private List<Product> FilterPrice (string price)
        {
            if (SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyPrice) == null)
            {
                List<string> priceList = new List<string>
                {
                    price
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, KeyPrice, priceList);
            }
            else
            {
                List<string> priceList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyPrice);
                string index = ExistPrice(price);
                if (index != "")
                    priceList.Remove(price);
                else
                    priceList.Add(price);
                SessionHelper.SetObjectAsJson(HttpContext.Session, KeyPrice, priceList);
                if (priceList.Count == 0)
                {
                    var itemLaptop = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
                    return itemLaptop;
                }
            }
            List<string> priceResult = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyPrice);
            List<Product> resultTemp = new List<Product>();
            List<Product> queryPrice = new List<Product>();
            foreach (var item in priceResult)
            {
                if (item == "20tr")
                {
                    queryPrice = dataContext.Products.Where(p => p.ProductPrice >= 20000000 && p.ProductPrice <= 30000000).ToList();
                }
                if (item == "60tr")
                {
                    queryPrice = dataContext.Products.Where(p => p.ProductPrice >= 60000000 && p.ProductPrice <= 70000000).ToList();
                }
                if (item == "120tr")
                {
                    queryPrice = dataContext.Products.Where(p => p.ProductPrice >= 120000000).ToList();
                }
                if (queryPrice != null)
                {
                    foreach (var itemPrice in queryPrice)
                    {
                        resultTemp.Add(itemPrice);
                    }
                }

            }
            CheckConflict(resultTemp);
            var pc = dataContext.PCs.ToList();
            List<Product> result = new List<Product>();
            for (int i = 0; i < resultTemp.Count; i++)
            {
                for (int j = 0; j < pc.Count; j++)
                {
                    if (resultTemp[i].ProductId == pc[j].ProductId)
                        result.Add(resultTemp[i]);
                }
            }
            return result;
        }
        public IActionResult DeleteFilter()
        {
            List<string> cpuList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyCPU);
            List<string> gpuList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyGPU);
            List<string> priceResult = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyPrice);
            SessionHelper.ClearSession(HttpContext.Session);
            var itemPC = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
            return View("Index", itemPC);
        }
        private List<Product> FilterCPU(string cpu)
        {
            if (SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyCPU) == null)
            {
                List<string> cpuList = new List<string>
                {
                    cpu
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, KeyCPU, cpuList);
            }
            else
            {
                List<string> cpuList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyCPU);
                string index = ExistCPU(cpu);
                if (index != "")
                    cpuList.Remove(cpu);
                else
                    cpuList.Add(cpu);
                SessionHelper.SetObjectAsJson(HttpContext.Session, KeyCPU, cpuList);
                if (cpuList.Count == 0)
                {
                    var itemLaptop = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
                    return itemLaptop;
                }
            }
            List<string> cpuResult = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyCPU);
            List<Product> result = new List<Product>();
            List<PC> resultPC = new List<PC>();
            foreach (var item in cpuResult)
            {
                var queryCPU = dataContext.PCs.Where(p => p.PCCpu.Contains(item));
                foreach (var itemCPU in queryCPU)
                {
                    resultPC.Add(itemCPU);
                }
            }
            CheckConflict(result);
            foreach (var item in resultPC)
            {
                Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                result.Add(product);
            }
            return result;
        }
        private List<Product> FilterGPU(string gpu)
        {
            if(SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyGPU) == null)
            {
                List<string> gpuList = new List<string>
                {
                    gpu
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, KeyCPU, gpuList);
            }
            else
            {
                List<string> gpuList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyGPU);
                string index = ExistGPU(gpu);
                if (index != "")
                    gpuList.Remove(gpu);
                else
                    gpuList.Add(gpu);
                SessionHelper.SetObjectAsJson(HttpContext.Session, KeyGPU, gpuList);
                if (gpuList.Count == 0)
                {
                    var itemLaptop = dataContext.Products.Where(p => p.CategoryId == 2).ToList();
                    return itemLaptop;
                }
            }
            List<string> gpuResult = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyGPU);
            List<Product> result = new List<Product>();
            List<PC> resultPC = new List<PC>();
            foreach (var item in gpuResult)
            {
                var queryGPU = dataContext.PCs.Where(p => p.PCGpu.Contains(item));
                foreach (var itemGPU in queryGPU)
                {
                    resultPC.Add(itemGPU);
                }
            }
            CheckConflict(result);
            foreach (var item in resultPC)
            {
                Product product = dataContext.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
                result.Add(product);
            }
            return result;
        }
        private string ExistCPU(string cpu)
        {
            List<string> cpuList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyCPU);
            for (int i = 0; i < cpuList.Count; i++)
            {
                if (cpuList[i] == cpu)
                    return cpu;
            }
            return "";
        }
        private string ExistPrice(string price)
        {
            List<string> priceList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyPrice);
            for (int i = 0; i < priceList.Count; i++)
            {
                if (priceList[i] == price)
                    return price;
            }
            return "";
        }
        private string ExistGPU(string gpu)
        {
            List<string> gpuList = SessionHelper.GetObjectFromJson<List<string>>(HttpContext.Session, KeyGPU);
            for (int i = 0; i < gpuList.Count; i++)
            {
                if (gpuList[i] == gpu)
                    return gpu;
            }
            return "";
        }
        private void CheckConflict(List<Product> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] == null)
                    break;
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (result[i].ProductId == result[j].ProductId)
                    {
                        result.RemoveAt(j);
                        break;
                    }
                }
            }
        }
    }
}
