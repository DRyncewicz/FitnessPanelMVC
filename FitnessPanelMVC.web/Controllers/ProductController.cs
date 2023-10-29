using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations;

namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValidator<NewProductVm> _validator;
        private readonly IFileService _fileService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(IProductService productService,
            IValidator<NewProductVm> validator,
            IFileService fileService,
            UserManager<IdentityUser> userManager)
        {
            _productService = productService;
            _validator = validator;
            _fileService = fileService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var model = _productService.GetAllProductsForList(20, 1, "", userId);
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
            var model = _productService.GetAllProductsForList(pageSize, pageNo.Value, searchString, userId);
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
            var userId = _userManager.GetUserId(User);
            var result = _validator.Validate(newProduct);
            if (result.IsValid)
            {
                _productService.AddNewProduct(newProduct, userId);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult EditProduct(int id)
        {
            var product = _productService.GetProductForEdit(id);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GetProductsFromExcel(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                _fileService.AddProductsFromFile(filePath, userId);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return RedirectToAction("Index");
        }

    }


}
