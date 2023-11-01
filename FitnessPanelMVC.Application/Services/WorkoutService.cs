﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Application.ViewModels.Workout;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Irony.Parsing;
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
                Where(i => i.Date.Date == Date.Date && i.UserId == userId)
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
        public int AddExerciseToWorkout(int exerciseId, int workoutId, int durationSeconds, double burnedCalories)
        {
            TimeSpan duration = TimeSpan.FromSeconds(durationSeconds);
            var exercise = _exerciseRepository.GetExercises()
                .FirstOrDefault(i => i.Id == exerciseId);
            if (_workoutExerciseRepository.GetAllWorkoutExercises()
                .Any(e => e.WorkoutId == workoutId && e.ExerciseId == exerciseId))
            {
                var workoutExercise = _workoutExerciseRepository.GetAllWorkoutExercises()
                    .First(e => e.WorkoutId == workoutId && e.ExerciseId == exerciseId);
                workoutExercise.Duration += duration;
                workoutExercise.CaloriesBurned += burnedCalories;

                _workoutExerciseRepository.UpdateWorkoutExercise(workoutExercise);
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

                _workoutExerciseRepository.CreateWorkoutExercise(workoutExercise);
            }

            UpdateWorkoutInformationsAfterProductChange(workoutId);
            return workoutId;
        }

        public void DeleteWorkoutById(int workoutId)
        {
            _workoutRepository.DeleteWorkout(workoutId);
        }

        public WorkoutForListVm GetWorkoutDetailsById(int workoutId)
        {
            var workout = _workoutRepository.GetAllWorkouts().FirstOrDefault(w => w.Id == workoutId);
            var workoutVm = _mapper.Map<WorkoutForListVm>(workout);

            return workoutVm;
        }

        public void DeleteExerciseFromWorkoutByIds(int workoutId, int exerciseId)
        {
            _workoutExerciseRepository.DeleteWorkoutExercise(workoutId, exerciseId);
        }

        private void UpdateWorkoutInformationsAfterProductChange(int workoutId)
        {
            var workout = _workoutRepository.GetAllWorkouts().FirstOrDefault(w => w.Id == workoutId);
            var workoutExercises = workout.WorkoutExercises.ToList();
            workout.TotalCaloriesBurned = workoutExercises.Select(w => w.CaloriesBurned).Sum();
            TimeSpan totalDuration = workoutExercises
                .Select(w => w.Duration)
                .Aggregate(TimeSpan.Zero, (subtotal, t) => subtotal + t);

            workout.Duration = totalDuration;
            _workoutRepository.UpdateWorkout(workout);
        }
    }
}
