using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Calendar;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    public class UserHealthController : Controller
    {
        private readonly IUserService _userService;

        private readonly IHealthStatsService _calendarService;

        public UserHealthController(IUserService userService, IHealthStatsService calendarService)
        {
            _userService = userService;
            _calendarService = calendarService;
        }
        public async Task<ActionResult> HealthAndStatistics()
        {
            var userId = await _userService.GetIdAsync(User);
            var model = await _calendarService.GetForListAsync(userId);
            return View(model);
        }
    }
}
