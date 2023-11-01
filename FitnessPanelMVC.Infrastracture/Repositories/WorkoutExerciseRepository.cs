using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class WorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        private readonly DbContext _dbContext;

        public WorkoutExerciseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(WorkoutExercise workoutExercise)
        {
            _dbContext.WorkoutExercise.Add(workoutExercise);
            _dbContext.SaveChanges();
        }

        public void Delete(int workoutId, int exerciseId)
        {
            var workoutExercise = _dbContext.WorkoutExercise.FirstOrDefault(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId);
            if (workoutExercise != null) 
            {
                _dbContext.WorkoutExercise.Remove(workoutExercise);
                _dbContext.SaveChanges();
            }
        }

        public void Update(WorkoutExercise workoutExercise)
        {
            if (_dbContext.WorkoutExercise.Find(workoutExercise.WorkoutId, workoutExercise.ExerciseId) != null)
            {
                _dbContext.WorkoutExercise.Update(workoutExercise);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<WorkoutExercise> GetAll()
        {
            var workoutExercises = _dbContext.WorkoutExercise;
            return workoutExercises.AsQueryable();
        }
    }
}
