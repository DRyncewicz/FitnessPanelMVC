using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.Meal;
using FitnessPanelMVC.Application.ViewModels.MealProduct.TransferModel;
using FitnessPanelMVC.Application.ViewModels.Product;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;


namespace FitnessPanelMVC.web.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IProductService _productService;

        public MealController(IMealService mealService, IProductService productService)
        {
            _mealService = mealService;
            _productService = productService;
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

        [HttpGet]
        public IActionResult AddMeal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMeal(NewMealVm newMealVm)
        {
            int id = _mealService.AddNewMeal(newMealVm);
            HttpContext.Session.SetInt32("CurrentMealId", id);
            return RedirectToAction("AddProductsToMealList");
        }

        [HttpGet]
        public IActionResult AddProductsToMealList()
        {
            var model = _productService.GetAllProductsForList(2, 1, "");
            return View(model);
        }
        [HttpPost]
        public IActionResult AddProductsToMealList(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _productService.GetAllProductsForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }
        [HttpPost]
        public IActionResult AddProductToMeal([FromBody] ProductMealModel model)
        {
            int mealId = HttpContext.Session.GetInt32("CurrentMealId") ?? default;
            _mealService.AddProductToMeal(model.ProductId, mealId, model.Weight);
            return Json(new { success = true, message = "Product added successfully" });
        }

        public IActionResult DeleteMeal(int id)
        {
            _mealService.DeleteMealById(id);
            return RedirectToAction("Index");
        }

        public IActionResult MealDetails(int id)
        {
            HttpContext.Session.SetInt32("CurrentMealId", id);
            var model = _mealService.GetMealDetailsById(id);
            return View(model);
        }

        public IActionResult DeleteProductFromMeal(int id, int mealId)
        {
            _mealService.DeleteProductFromMealById(id, mealId);
            return RedirectToAction("Index");
        }
    }
}
