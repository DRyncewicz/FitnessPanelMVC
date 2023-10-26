using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FitnessPanelMVC.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValidator<NewProductVm> _validator;

        public ProductController(IProductService productService, IValidator<NewProductVm> validator)
        {
            _productService = productService;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _productService.GetAllProductsForList(2, 1, "");
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
            var model = _productService.GetAllProductsForList(pageSize, pageNo.Value, searchString);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(NewProductVm newProduct)
        {
            var result = _validator.Validate(newProduct);
            if (result.IsValid)
            {
                _productService.AddNewProduct(newProduct);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _productService.GetProductForEdit(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(NewProductVm editedProduct)
        {
            var result = _validator.Validate(editedProduct);
            if (result.IsValid)
            {
                _productService.UpdateProduct(editedProduct);
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }

        public IActionResult ProductDetails(int id)
        {

            var productDetails = _productService.GetProductDetailsById(id);
            return View(productDetails);
        }
    }
}
