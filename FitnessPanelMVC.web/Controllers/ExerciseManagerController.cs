using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.ExternalExercise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessPanelMVC.web.Controllers
{
    [Authorize]
    public class ExerciseManagerController : Controller
    {
        private readonly IExerciseApiService _exerciseApiService;

        public ExerciseManagerController(IExerciseApiService exerciseApiService)
        {
            _exerciseApiService = exerciseApiService;
        }

        public IActionResult Index()
        {
            var model = new ExerciseOptionsVm
            {
                AvailableOptions = new List<string>
            {
                "back",
                "cardio",
                "chest",
                "lower arms",
                "lower legs",
                "neck",
                "shoulders",
                "upper arms",
                "upper legs",
                "waist"
            },
                ExerciseResults = new List<ExternalExerciseVm>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetExerciseResult(ExerciseOptionsVm model)
        {
            model.AvailableOptions = new List<string>
        {
            "back",
            "cardio",
            "chest",
            "lower arms",
            "lower legs",
            "neck",
            "shoulders",
            "upper arms",
            "upper legs",
            "waist"
        };

            if (!string.IsNullOrEmpty(model.SelectedOption))
            {
                var result = await _exerciseApiService.GetExerciseVmToList(model.SelectedOption);
                model.ExerciseResults = result;
            }

            return View("Index", model);
        }
    }
}