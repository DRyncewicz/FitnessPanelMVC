using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IWorkoutRepository
    {
        public int Create(Workout workout);

        public void Delete(int id);

        public IQueryable<Workout> GetAll();

        public int Update(Workout workout);
    }
}
