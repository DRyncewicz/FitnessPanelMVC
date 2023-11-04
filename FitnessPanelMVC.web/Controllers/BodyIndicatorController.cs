using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.Validators;
using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Model;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class BodyIndicatorController : Controller
    {
        private readonly IBodyIndicatorService _bodyIndicatorService;

        private readonly IUserReportService _userReportService;

        private readonly IValidator<NewBodyIndicatorVm> _validator;

        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly UserManager<ApplicationUser> _userManager;

        public BodyIndicatorController(IBodyIndicatorService bodyIndicatorService,
            IUserReportService userReportService,
            IValidator<NewBodyIndicatorVm> validator,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment hostEnvironment)
        {
            _bodyIndicatorService = bodyIndicatorService;
            _userReportService = userReportService;
            _validator = validator;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            var model = await _userReportService.GetUserBodyReportsAsync(userId);
            return View(model);
        }

        public FileResult DownloadReport(string filePath)
        {
            var mimeType = "application/pdf";
            var fileFullPath = Path.Combine(_hostEnvironment.WebRootPath, filePath);
            var fileBytes = System.IO.File.ReadAllBytes(fileFullPath);
            var fileName = Path.GetFileName(filePath);

            return File(fileBytes, mimeType, fileName);
        }

        [HttpGet]
        public IActionResult CreateBodyIndicator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBodyIndicator(NewBodyIndicatorVm newBodyIndicatorVm)
        {
            var userId = _userManager.GetUserId(User);
            var result = _validator.Validate(newBodyIndicatorVm);
            if (result.IsValid)
            {
                int id = _bodyIndicatorService.AddNew(newBodyIndicatorVm);
                var bodyIndicator = _bodyIndicatorService.GetById(id);
                await _userReportService.CreateUserBodyReportAsync(bodyIndicator, userId);

            }
            return View();
        }
    }
}
