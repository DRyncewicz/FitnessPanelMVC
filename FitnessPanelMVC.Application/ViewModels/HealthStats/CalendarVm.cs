using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Calendar
{
    public class CalendarVm
    {
        public List<EventVm> Events { get; set; } = new List<EventVm>();
    }
}
