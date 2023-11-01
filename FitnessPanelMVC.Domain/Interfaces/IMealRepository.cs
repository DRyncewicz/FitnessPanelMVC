using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IMealRepository
    {
        public int Create(Meal meal);

        public int Update(Meal meal); 

        public void Delete(int id);

        public IQueryable<Meal> GetAll();
    }
}
