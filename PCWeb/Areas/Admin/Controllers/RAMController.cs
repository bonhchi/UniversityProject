using Microsoft.AspNetCore.Mvc;

namespace PCWeb.Areas.Admin.Controllers
{
    public class RAMController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
