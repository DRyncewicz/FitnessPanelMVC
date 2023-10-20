using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class WorkoutExerciseRepository : IWorkoutExerciseRepository<WorkoutExercise>
    {
        private readonly Context _dbContext;
        public WorkoutExerciseRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateWorkoutExercise(WorkoutExercise workoutExercise)
        {
            _dbContext.WorkoutExercise.Add(workoutExercise);
            _dbContext.SaveChanges();
            return workoutExercise.Id;
        }

        public void DeleteWorkoutExercise(int id)
        {
            var workoutExercise = _dbContext.WorkoutExercise.Find(id);
            if (workoutExercise != null) 
            {
                _dbContext.WorkoutExercise.Remove(workoutExercise);
                _dbContext.SaveChanges();
            }
        }

        public int UpdateWorkoutExercise(WorkoutExercise workoutExercise)
        {
            if (_dbContext.WorkoutExercise.Find(workoutExercise.Id) != null)
            {
                _dbContext.WorkoutExercise.Update(workoutExercise);
                _dbContext.SaveChanges();
                return workoutExercise.Id;
            }
            return 0;
        }
        public IQueryable<WorkoutExercise> GetWorkoutExercisesByWorkoutId(int workoutId)
        {
            var workoutExercises = _dbContext.WorkoutExercise.Where(x  => x.WorkoutId == workoutId);
            return workoutExercises.AsQueryable();
        }
    }
}
