using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IBodyIndicatorService
    {
        int AddNewBodyIndicator(NewBodyIndicatorVm bMIFormVm);
        BodyIndicator GetBodyIndicatorById(int bodyIndicatorId);
    }
}
