using FitnessPanelMVC.Application.ViewModels.BodyIndicator;
using FitnessPanelMVC.Application.ViewModels.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Validators
{
    public class NewBodyIndicatorValidation : AbstractValidator<NewBodyIndicatorVm>
    {
        public NewBodyIndicatorValidation()
        {
            RuleFor(m => m.Age).InclusiveBetween(10, 99)
                .WithMessage("Age must be between 10 and 99");
            RuleFor(m => m.Height).InclusiveBetween(80, 250)
                .WithMessage("Height must be between 80 and 250cm");
            RuleFor(m => m.Sex).NotNull();
            RuleFor(m => m.HipCircumference).InclusiveBetween(40, 150);
            RuleFor(m => m.AbdominalCircumference).InclusiveBetween(40, 150);
        }
    }
}
