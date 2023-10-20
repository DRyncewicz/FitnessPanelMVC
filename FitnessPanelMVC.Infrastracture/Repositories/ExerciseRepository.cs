using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository<Exercise>
    {
        private readonly Context _dbCotenxt;
        public ExerciseRepository(Context dbContext)
        {
            _dbCotenxt = dbContext;
        }

        public int CreateExercise(Exercise exercise)
        {
            _dbCotenxt.Exercise.Add(exercise);
            _dbCotenxt.SaveChanges();
            return exercise.Id;
        }

        public void DeleteExercise(int id) 
        {
            var exercise = _dbCotenxt.Exercise.Find(id);
            if (exercise != null)
            {
                _dbCotenxt.Exercise.Remove(exercise);
                _dbCotenxt.SaveChanges();
            }
        }

        public int UpdateExercise(Exercise exercise)
        {
            if (_dbCotenxt.Exercise.Find(exercise.Id) != null)
            {
                _dbCotenxt.Exercise.Update(exercise);
                _dbCotenxt.SaveChanges();
                return exercise.Id;
            }
            return 0;
        }

        public IQueryable<Exercise> GetExercises()
        {
            var exercises = _dbCotenxt.Exercise;
            return exercises;
        }

        public IQueryable<Exercise> GetExercisesByNameContains(string name)
        {
            var exercises = _dbCotenxt.Exercise.Where(x => x.Name.Contains(name));
            return exercises;
        }


    }
}
