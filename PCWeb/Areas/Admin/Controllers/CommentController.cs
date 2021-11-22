using Microsoft.AspNetCore.Mvc;

namespace PCWeb.Areas.Admin.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
