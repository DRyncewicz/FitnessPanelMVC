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

        private readonly IUserService _userSerivce;

        public MealController(IMealService mealService, IProductService productService, IUserService userService)
        {
            _mealService = mealService;
            _productService = productService;
            _userSerivce = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = await _userSerivce.GetIdAsync(User);
            var model = await _mealService.GetForListByDate(DateTime.Now, userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime date)
        {
            var userId = await _userSerivce.GetIdAsync(User);
            var model = await _mealService.GetForListByDate(date, userId);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddMeal()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMeal(NewMealVm newMealVm)
        {
            var userId = await _userSerivce.GetIdAsync(User);
            int id = await _mealService.AddNewAsync(newMealVm, userId);
            HttpContext.Session.SetInt32("CurrentMealId", id);
            return RedirectToAction("AddProductsToMealList");
        }

        [HttpGet]
        public async Task<IActionResult> AddProductsToMealList()
        {
            var userId = await _userSerivce.GetIdAsync(User);
            var model = await _productService.GetAllForListAsync(20, 1, "", userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductsToMealList(int pageSize, int? pageNo, string searchString)
        {
            var userId = await _userSerivce.GetIdAsync(User);
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = await _productService.GetAllForListAsync(pageSize, pageNo.Value, searchString, userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToMeal([FromBody] ProductMealModel model)
        {
            int mealId = HttpContext.Session.GetInt32("CurrentMealId") ?? default;
            await _mealService.AddProductToMealAsync(model.ProductId, mealId, model.Weight);
            return Json(new { success = true, message = "Product added successfully" });
        }

        public async Task<IActionResult> DeleteMeal(int id)
        {
            await _mealService.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MealDetails(int id)
        {
            HttpContext.Session.SetInt32("CurrentMealId", id);
            var model = await _mealService.GetDetailsByIdAsync(id);
            return View(model);
        }

        public async Task<IActionResult> DeleteProductFromMeal(int id, int mealId)
        {
            await _mealService.DeleteProductFromMealByIdAsync(id, mealId);
            return RedirectToAction("Index");
        }
    }
}
