using FitnessPanelMVC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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

    }
}
