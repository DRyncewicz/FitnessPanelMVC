using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.MealProduct.TransferModel;
using FitnessPanelMVC.Application.ViewModels.Recipe;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IProductService _productService;
        public RecipeController(IRecipeService recipeService, IProductService productService)
        {
            _recipeService = recipeService;
            _productService = productService;
        }
        public IActionResult Index()
        {
            var model = _recipeService.GetRecipesForList(10, 1, "");
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int pageSize, int? pageNo, string searchString)
        {
            if (!pageNo.HasValue)
            {
                pageNo = 1;
            }
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            var model = _recipeService.GetRecipesForList(pageSize, pageNo.Value, searchString);
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
            int id = _recipeService.AddNewRecipe(newRecipeVm);
            HttpContext.Session.SetInt32("CurrentRecipeId", id);
            return RedirectToAction("AddProductsToRecipeList");
        }

        [HttpGet]
        public IActionResult AddProductsToRecipeList()
        {
            var model = _productService.GetAllProductsForList(20, 1, "");
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProductsToRecipeList(int pageSize, int? pageNo, string searchString)
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
