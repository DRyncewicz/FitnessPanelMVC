using FitnessPanelMVC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IExerciseService _exerciseService;
        public WorkoutController(IWorkoutService workoutService,
            UserManager<IdentityUser> userManager,
            IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
            _workoutService = workoutService;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var model = _workoutService.GetAllUserWorkoutsForList(DateTime.Now, userId);
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(DateTime date)
        {
            var userId = _userManager.GetUserId(User);
            var model = _workoutService.GetAllUserWorkoutsForList(date, userId);
            return View(model);
        }
        [HttpGet]
        public IActionResult AddWorkout()
        {
            return View();
        }
    }
}
