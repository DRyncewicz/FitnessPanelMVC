using FitnessPanelMVC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        public IActionResult Index(DateTime date)
        {
            var model = _mealService.GetMealsForListByDate(new DateTime(2023, 10, 11));
            return View(model);
        }
    }
}
