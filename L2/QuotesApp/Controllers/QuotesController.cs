using Microsoft.AspNetCore.Mvc;

namespace QuotesApp.Controllers
{
    public class QuotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
