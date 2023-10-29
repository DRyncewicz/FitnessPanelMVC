using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.MealProduct.TransferModel;
using FitnessPanelMVC.Application.ViewModels.Recipe;
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
        private readonly UserManager<IdentityUser> _userManager;
        public RecipeController(
            IRecipeService recipeService,
            IProductService productService,
            UserManager<IdentityUser> userManager)
        {
            _recipeService = recipeService;
            _productService = productService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var model = _recipeService.GetRecipesForList(10, 1, "", userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
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
            var model = _recipeService.GetRecipesForList(pageSize, pageNo.Value, searchString, userId);
            return View(model);
        }

        public IActionResult DeleteRecipe(int id)
        {
            _recipeService.DeleteRecipe(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRecipe(NewRecipeVm newRecipeVm)
        {
            var userId = _userManager.GetUserId(User);
            int id = _recipeService.AddNewRecipe(newRecipeVm, userId);
            HttpContext.Session.SetInt32("CurrentRecipeId", id);
            return RedirectToAction("AddProductsToRecipeList");
        }

        [HttpGet]
        public IActionResult AddProductsToRecipeList()
        {
            var userId = _userManager.GetUserId(User);
            var model = _productService.GetAllProductsForList(20, 1, "", userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductsToRecipeList(int pageSize, int? pageNo, string searchString)
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
            var model = _productService.GetAllProductsForList(pageSize, pageNo.Value, searchString, userId);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductToRecipe([FromBody] ProductMealModel model)
        {
            int recipeId = HttpContext.Session.GetInt32("CurrentRecipeId") ?? default;
            _recipeService.AddProductToRecipe(model.ProductId, recipeId, model.Weight);
            var productVm = _recipeService.UpdateProductOnRecipeChange(recipeId);
            _productService.UpdateProduct(productVm);
            return Json(new { success = true, message = "Product added successfully" });
        }
    }
}
