using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Meal;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessPanelMVC.web.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _mealService.GetMealsForListByDate(DateTime.Now);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DateTime date)
        {
            var model = _mealService.GetMealsForListByDate(date);
            return View(model);
        }
    }
}
