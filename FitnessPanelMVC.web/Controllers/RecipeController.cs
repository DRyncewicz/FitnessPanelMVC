using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
