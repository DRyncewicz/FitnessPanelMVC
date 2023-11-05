using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.HealthStats
{
    public class ChartVm
    {
        public List<double> LastWeekCalories {  get; set; }

        public List<double> LastWeekWorkoutTime { get; set; }
    }
}
