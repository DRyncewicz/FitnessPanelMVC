using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.Validators;
using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Application.ViewModels.Product;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class BodyIndicatorController : Controller
    {
        private readonly IBodyIndicatorService _bodyIndicatorService;
        private readonly IFileService _fileService;
        private readonly IValidator<NewBodyIndicatorVm> _validator;
        public BodyIndicatorController(IBodyIndicatorService bodyIndicatorService, IFileService fileService, IValidator<NewBodyIndicatorVm> validator)
        {
            _bodyIndicatorService = bodyIndicatorService;
            _fileService = fileService;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult CreateBodyIndicator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBodyIndicator(NewBodyIndicatorVm BMIFormVm)
        {
            var result = _validator.Validate(BMIFormVm);
            if (result.IsValid)
            {
                int id = _bodyIndicatorService.AddNewBodyIndicator(BMIFormVm);
                var bodyIndicator = _bodyIndicatorService.GetBodyIndicatorById(id);
                var report = _fileService.GenerateBodyMetricsReport(bodyIndicator);
                return File(report, "application/pdf", "Raport.pdf");
            }
            return View();
        }
    }
}
