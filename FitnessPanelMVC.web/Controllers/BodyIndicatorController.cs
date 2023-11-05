using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Application.Validators;
using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Application.ViewModels.UserReportFile;
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

        private readonly IUserService _userService;

        public BodyIndicatorController(IBodyIndicatorService bodyIndicatorService,
            IUserReportService userReportService,
            IValidator<NewBodyIndicatorVm> validator,
            IUserService userService,
            IWebHostEnvironment hostEnvironment)
        {
            _bodyIndicatorService = bodyIndicatorService;
            _userReportService = userReportService;
            _validator = validator;
            _userService = userService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userVm = await _userService.GetAsync(User);
            var model = await _userReportService.GetUserBodyReportsAsync(userVm);

            return View(model);
        }

        public async Task<FileResult> DownloadReport(string filePath)
        {
            var mimeType = "application/pdf";
            var fileFullPath = Path.Combine(_hostEnvironment.WebRootPath, filePath);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(fileFullPath);
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
            var userId = await _userService.GetIdAsync(User);
            var result = await _validator.ValidateAsync(newBodyIndicatorVm);
            if (result.IsValid)
            {
                int id = await _bodyIndicatorService.AddNewAsync(newBodyIndicatorVm);
                var bodyIndicator = await _bodyIndicatorService.GetByIdAsync(id);
                await _userReportService.CreateUserBodyReportAsync(bodyIndicator, userId);
            }
            return View();
        }
    }
}
