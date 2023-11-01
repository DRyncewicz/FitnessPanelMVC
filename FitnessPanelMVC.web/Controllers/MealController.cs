using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.Meal;
using FitnessPanelMVC.Application.ViewModels.MealProduct.TransferModel;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly IMealService _mealService;

        private readonly IProductService _productService;

        private readonly UserManager<ApplicationUser> _userManager;

        public MealController(IMealService mealService, IProductService productService, UserManager<ApplicationUser> userManager)
        {
            _mealService = mealService;
            _productService = productService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var model = _mealService.GetForListByDate(DateTime.Now, userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DateTime date)
        {
            var userId = _userManager.GetUserId(User);
            var model = _mealService.GetForListByDate(date, userId);
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
            var userId = _userManager.GetUserId(User);
            int id = _mealService.AddNew(newMealVm, userId);
            HttpContext.Session.SetInt32("CurrentMealId", id);
            return RedirectToAction("AddProductsToMealList");
        }

        [HttpGet]
        public IActionResult AddProductsToMealList()
        {
            var userId = _userManager.GetUserId(User);
            var model = _productService.GetAllForList(20, 1, "", userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductsToMealList(int pageSize, int? pageNo, string searchString)
        {
            var userId = _userManager.GetUserId(User);
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _productService.GetAllForList(pageSize, pageNo.Value, searchString, userId);
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
            _mealService.DeleteById(id);
            return RedirectToAction("Index");
        }

        public IActionResult MealDetails(int id)
        {
            HttpContext.Session.SetInt32("CurrentMealId", id);
            var model = _mealService.GetDetailsById(id);
            return View(model);
        }

        public IActionResult DeleteProductFromMeal(int id, int mealId)
        {
            _mealService.DeleteProductFromMealById(id, mealId);
            return RedirectToAction("Index");
        }
    }
}
