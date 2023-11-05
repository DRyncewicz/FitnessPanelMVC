using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Application.ViewModels.Workout;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<WorkoutForListVm>> GetAllForListAsync(DateTime Date, string userId)
        {
            var workouts =await _workoutRepository.GetAll().
                Where(i => i.Date.Date == Date.Date && i.UserId == userId)
                .ProjectTo<WorkoutForListVm>(_mapper.ConfigurationProvider).ToListAsync();

            return workouts;
        }

        public async Task<int> AddNewAsync(NewWorkoutVm workoutVm, string userId)
        {
            var workout = _mapper.Map<Workout>(workoutVm);
            workout.UserId = userId;
            await _workoutRepository.CreateAsync(workout);

            return workout.Id;
        }

        public async Task<int> AddExerciseToWorkoutAsync(int exerciseId, int workoutId, int durationSeconds, double burnedCalories)
        {
            TimeSpan duration = TimeSpan.FromSeconds(durationSeconds);
            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            if (await _workoutExerciseRepository.GetAll()
                .AnyAsync(e => e.WorkoutId == workoutId && e.ExerciseId == exerciseId))
            {
                var workoutExercise = _workoutExerciseRepository.GetAll()
                    .First(e => e.WorkoutId == workoutId && e.ExerciseId == exerciseId);
                workoutExercise.Duration += duration;
                workoutExercise.CaloriesBurned += burnedCalories;

                await _workoutExerciseRepository.UpdateAsync(workoutExercise);
            }
            else
            {
                var workoutExercise = new WorkoutExercise
                {
                    WorkoutId = workoutId,
                    ExerciseId = exerciseId,
                    Duration = duration,
                    CaloriesBurned = burnedCalories
                };

                await _workoutExerciseRepository.CreateAsync(workoutExercise);
            }

            await UpdateWorkoutInformationsAfterProductChangeAsync(workoutId);
            return workoutId;
        }

        public async Task DeleteByIdAsync(int workoutId)
        {
            await _workoutRepository.DeleteAsync(workoutId);
        }

        public async Task<WorkoutForListVm> GetDetailsByIdAsync(int workoutId)
        {
            var workout = await _workoutRepository.GetAll().FirstOrDefaultAsync(w => w.Id == workoutId);
            var workoutVm = _mapper.Map<WorkoutForListVm>(workout);

            return workoutVm;
        }

        public async Task DeleteExerciseFromWorkoutByIdsAsync(int workoutId, int exerciseId)
        {
            await _workoutExerciseRepository.DeleteAsync(workoutId, exerciseId);
        }

        private async Task UpdateWorkoutInformationsAfterProductChangeAsync(int workoutId)
        {
            var workout = await _workoutRepository.GetByIdAsync(workoutId);
            var workoutExercises = workout.WorkoutExercises.ToList();
            workout.TotalCaloriesBurned = workoutExercises.Select(w => w.CaloriesBurned).Sum();
            TimeSpan totalDuration = workoutExercises
                .Select(w => w.Duration)
                .Aggregate(TimeSpan.Zero, (subtotal, t) => subtotal + t);

            workout.Duration = totalDuration;
            await _workoutRepository.UpdateAsync(workout);
        }
    }
}
