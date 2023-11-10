using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Domain.Model;
using FitnessPanelMVC.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessPanelMVC.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        private readonly IBodyIndicatorService _bodyIndicatorService;
        public HomeController(IUserService userService, IBodyIndicatorService bodyIndicatorService)
        {
            _userService = userService;
            _bodyIndicatorService = bodyIndicatorService;
        }

        public async Task<IActionResult> Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var userId = await _userService.GetIdAsync(User);
                var model = await _bodyIndicatorService.GetByUserIdAsync(userId);
                return View();
            }
            else
            {
                return View();           
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}