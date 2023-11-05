using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.MealProduct.TransferModel;
using FitnessPanelMVC.Application.ViewModels.Recipe;
using FitnessPanelMVC.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        private readonly IProductService _productService;

        private readonly IUserService _userService;

        public RecipeController(
            IRecipeService recipeService,
            IProductService productService,
            IUserService userService)
        {
            _recipeService = recipeService;
            _productService = productService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = await _userService.GetIdAsync(User);
            var model = await _recipeService.GetForListAsync(10, 1, "", userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int pageSize, int? pageNo, string searchString)
        {
            var userId = await _userService.GetIdAsync(User);
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = await _recipeService.GetForListAsync(pageSize, pageNo.Value, searchString, userId);
            return View(model);
        }

        public async Task<IActionResult> DeleteRecipe(int id)
        {
            await _recipeService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(NewRecipeVm newRecipeVm)
        {
            var userId = await _userService.GetIdAsync(User);
            int id = await _recipeService.AddNewAsync(newRecipeVm, userId);
            HttpContext.Session.SetInt32("CurrentRecipeId", id);
            return RedirectToAction("AddProductsToRecipeList");
        }

        [HttpGet]
        public async Task<IActionResult> AddProductsToRecipeList()
        {
            var userId = await _userService.GetIdAsync(User);
            var model = await _productService.GetAllForListAsync(20, 1, "", userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductsToRecipeList(int pageSize, int? pageNo, string searchString)
        {
            
            var userId = await _userService.GetIdAsync(User);
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
        public async Task<IActionResult> AddProductToRecipe([FromBody] ProductMealModel model)
        {
            int recipeId = HttpContext.Session.GetInt32("CurrentRecipeId") ?? default;
            await _recipeService.AddProductToRecipeAsync(model.ProductId, recipeId, model.Weight);
            var productVm =await _recipeService.UpdateProductOnRecipeChangeAsync(recipeId);
            await _productService.UpdateAsync(productVm);
            return Json(new { success = true, message = "Product added successfully" });
        }

        public async Task<IActionResult> RecipeDetails(int id)
        {
            HttpContext.Session.SetInt32("CurrentRecipeId", id);
            var model = await _recipeService.GetDetailsByIdAsync(id);
            return View(model);
        }

        public IActionResult DeleteProductFromRecipe (int productId, int recipeId)
        {
            _recipeService.DeleteProductFromMealByIdsAsync(productId, recipeId);
            return RedirectToAction("Index");
        }
    }
}
