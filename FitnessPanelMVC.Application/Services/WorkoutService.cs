using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Application.ViewModels.Workout;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IMapper _mapper;
        private readonly IWorkoutExerciseRepository _workoutExerciseRepository;
        public WorkoutService(IExerciseRepository exerciseRepository,
            IMapper mapper,
            IWorkoutRepository workoutRepository,
            IWorkoutExerciseRepository workoutExerciseRepository)
        {
            _workoutExerciseRepository = workoutExerciseRepository;
            _mapper = mapper;
            _workoutRepository = workoutRepository;
            _exerciseRepository = exerciseRepository;
        }

        public List<WorkoutForListVm> GetAllUserWorkoutsForList(DateTime Date, string userId)
        {
            var workouts = _workoutRepository.GetAllWorkouts().
                Where(i => i.Date == Date && i.UserId ==  userId)
                .ProjectTo<WorkoutForListVm>(_mapper.ConfigurationProvider).ToList();

            return workouts;
        }

        public int AddNewWorkout(NewWorkoutVm workoutVm, string userId)
        {
            var workout = _mapper.Map<Workout>(workoutVm);
            workout.UserId = userId;
            _workoutRepository.CreateWorkout(workout);

            return workout.Id;
        }
    }
}
