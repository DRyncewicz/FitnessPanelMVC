using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.BMI;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    public class BMIController : Controller
    {
        private readonly IBMIService _bMIService;
        public BMIController(IBMIService bMIService)
        {
            _bMIService = bMIService;
        }

        public IActionResult Index() 
        { 
            return View(); 
        }
        [HttpGet]
        public IActionResult CreateBMIProfile()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBMIProfile(BMIFormVm BMIFormVm)
        {

            return Index();
        }
    }
}
