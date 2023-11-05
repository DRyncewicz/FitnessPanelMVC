using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private readonly IValidator<NewProductVm> _validator;

        private readonly IUserReportService _fileService;

        private readonly IUserService _userSerivce;

        public ProductController(IProductService productService,
            IValidator<NewProductVm> validator,
            IUserReportService fileService,
            IUserService userService)
        {
            _productService = productService;
            _validator = validator;
            _fileService = fileService;
            _userSerivce = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = await _userSerivce.GetIdAsync(User);
            var model = await _productService.GetAllForListAsync(20, 1, "", userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int pageSize, int? pageNo, string searchString)
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

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(NewProductVm newProduct)
        {
            var userId = await _userSerivce.GetIdAsync(User);
            var result = _validator.Validate(newProduct);
            if (result.IsValid)
            {
                await _productService.AddNewAsync(newProduct, userId);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetForEditAsync(id);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(NewProductVm editedProduct)
        {
            var result = await _validator.ValidateAsync(editedProduct);
            if (result.IsValid)
            {
                await _productService.UpdateAsync(editedProduct);
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProductDetails(int id)
        {

            var productDetails = await _productService.GetDetailsByIdAsync(id);
            return View(productDetails);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProductsFromExcel(IFormFile file)
        {
            var userId = await _userSerivce.GetIdAsync(User);
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                await _productService.AddFromXmlFileAsync(filePath, userId);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return RedirectToAction("Index");
        }

    }


}
