using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Exercise;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _mapper = mapper;
        }

        public List<ExerciseForListAndNewVm> GetExercisesForLists()
        {
            var exercisesVm = _exerciseRepository.GetExercises()
                .ProjectTo<ExerciseForListAndNewVm>(_mapper.ConfigurationProvider).ToList();
            return exercisesVm;
        }

        public int AddNewExercise(ExerciseForListAndNewVm exerciseVm,string userId)
        {
            var exercise = _mapper.Map<Exercise>(exerciseVm);
            _exerciseRepository.CreateExercise(exercise);
            return exercise.Id;
        }
    }
}
