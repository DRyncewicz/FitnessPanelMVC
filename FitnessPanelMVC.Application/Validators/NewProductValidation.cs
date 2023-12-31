﻿using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Interface;
using FluentValidation;

namespace FitnessPanelMVC.Application.Validators
{
    public class NewProductValidation : AbstractValidator<NewProductVm>
    {
        public NewProductValidation(IProductRepository productRepository)
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).MaximumLength(255);
            RuleFor(x => x.Name).Must((product, name) => 
                !productRepository.GetAll()
                .Any(p => p.Name == name && p.Id != product.Id))
                .WithMessage("Product name must be unique.");
        }
    }
}
