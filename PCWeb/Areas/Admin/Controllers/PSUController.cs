using Microsoft.AspNetCore.Mvc;

namespace PCWeb.Areas.Admin.Controllers
{
    public class PSUController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
