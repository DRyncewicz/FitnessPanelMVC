using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    public class BodyIndicatorController : Controller
    {
        private readonly IBodyIndicatorService _bodyIndicatorService;
        private readonly IFileService _fileService;
        public BodyIndicatorController(IBodyIndicatorService bodyIndicatorService, IFileService fileService)
        {
            _bodyIndicatorService = bodyIndicatorService;
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult CreateBodyIndicator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBodyIndicator(NewBodyIndicatorVm BMIFormVm)
        {
            int id = _bodyIndicatorService.AddNewBodyIndicator(BMIFormVm);
            var bodyIndicator = _bodyIndicatorService.GetBodyIndicatorById(id);
            var report = _fileService.GenerateBodyMetricsReport(bodyIndicator);
            return File(report, "application/pdf", "Raport.pdf");
        }
    }
}
