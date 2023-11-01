using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Workout;
using FitnessPanelMVC.Application.ViewModels.WorkoutExercise.TransferModel;
using Humanizer.Localisation.DateToOrdinalWords;
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
        [HttpPost]
        public IActionResult AddWorkout(NewWorkoutVm newWorkoutVm)
        {
            var userId = _userManager.GetUserId(User);
            int id = _workoutService.AddNewWorkout(newWorkoutVm, userId);
            HttpContext.Session.SetInt32("CurrentWorkoutId", id);
            return RedirectToAction("AddExercisesToWorkoutList");
        }
        [HttpGet]
        public IActionResult AddExercisesToWorkoutList()
        {
            var model = _exerciseService.GetExercisesForLists();
            return View(model);
        }

        [HttpPost]
        public IActionResult FinishExercise([FromBody] WorkoutExerciseModel model)
        {
            int workoutId = HttpContext.Session.GetInt32("CurrentWorkoutId") ?? default;
            _workoutService.AddExerciseToWorkout(model.ExerciseId, workoutId, model.DurationSeconds, model.BurnedCalories);
            return Json(new { success = true, message = "Exercise added successfully." });
        }

        public IActionResult DeleteWorkout(int id)
        {
            _workoutService.DeleteWorkoutById(id);
            return RedirectToAction("Index");
        }

        public IActionResult WorkoutDetails(int id)
        {
            HttpContext.Session.SetInt32("CurrentWorkoutId", id);
            var  model = _workoutService.GetWorkoutDetailsById(id);
            return View(model);
        }

        public IActionResult DeleteExerciseFromWorkout(int workoutId, int exerciseId)
        {
            _workoutService.DeleteExerciseFromWorkoutByIds(workoutId, exerciseId);
            return RedirectToAction("Index");
        }
    }
}
