using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Meal
{
    public class ListMealForListVm
    {
        public List<MealForListVm> Meals { get; set; }

        public int Count { get; set; }
    }
}
