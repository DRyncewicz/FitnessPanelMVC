using FitnessPanelMVC.Application.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Workout
{
    public class NewWorkoutVm :IMapFrom<FitnessPanelMVC.Domain.Model.Workout>
    {
        public int Id { get; set; }
    }
}
