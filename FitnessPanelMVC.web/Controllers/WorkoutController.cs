using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Workout;
using FitnessPanelMVC.Application.ViewModels.WorkoutExercise.TransferModel;
using FitnessPanelMVC.Domain.Model;
using Humanizer.Localisation.DateToOrdinalWords;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;

        private readonly IUserService _userService;

        private readonly IExerciseService _exerciseService;

        public WorkoutController(IWorkoutService workoutService,
            IUserService userService,
            IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
            _workoutService = workoutService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = await _userService.GetIdAsync(User);
            var model = await _workoutService.GetAllForListAsync(DateTime.Now, userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime date)
        {
            var userId = await _userService.GetIdAsync(User);
            var model = await _workoutService.GetAllForListAsync(date, userId);
            return View(model);
        }

        [HttpGet]
        public IActionResult AddWorkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout(NewWorkoutVm newWorkoutVm)
        {
            var userId = await _userService.GetIdAsync(User);
            int id = await _workoutService.AddNewAsync(newWorkoutVm, userId);
            HttpContext.Session.SetInt32("CurrentWorkoutId", id);
            return RedirectToAction("AddExercisesToWorkoutList");
        }

        [HttpGet]
        public IActionResult AddExercisesToWorkoutList()
        {
            var model = _exerciseService.GetForList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FinishExercise([FromBody] WorkoutExerciseModel model)
        {
            int workoutId = HttpContext.Session.GetInt32("CurrentWorkoutId") ?? default;
            await _workoutService.AddExerciseToWorkoutAsync(model.ExerciseId, workoutId, model.DurationSeconds, model.BurnedCalories);
            return Json(new { success = true, message = "Exercise added successfully." });
        }

        public async Task<IActionResult> DeleteWorkout(int id)
        {
            await _workoutService.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> WorkoutDetails(int id)
        {
            HttpContext.Session.SetInt32("CurrentWorkoutId", id);
            var model = await _workoutService.GetDetailsByIdAsync(id);
            return View(model);
        }

        public async Task<IActionResult> DeleteExerciseFromWorkout(int workoutId, int exerciseId)
        {
            await _workoutService.DeleteExerciseFromWorkoutByIdsAsync(workoutId, exerciseId);
            return RedirectToAction("Index");
        }
    }
}
