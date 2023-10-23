using FitnessPanelMVC.Application.ViewModels.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IMealService
    {
        ListMealForListVm GetMealsForListByDate(DateTime date);
    }
}
