using ClosedXML.Excel;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.ExternalExercise;
using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class ExerciseApiService : IExerciseApiService
    {
        private readonly IExerciseApiClient _exerciseApiClient;

        public ExerciseApiService(IExerciseApiClient exerciseApiClient)
        {
            _exerciseApiClient = exerciseApiClient;
        }

        public async Task<List<ExternalExerciseVm>> GetExerciseVmToList(string bodyPartName)
        {
            var dataFromJson = await _exerciseApiClient.GetExerciseByBodyPartAsync(bodyPartName);
            var result = await dataFromJson.Content.ReadFromJsonAsync<List<ExternalExerciseVm>>();

            return result;
        }
    }
}
