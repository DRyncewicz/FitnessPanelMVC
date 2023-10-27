using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.Services;
using FitnessPanelMVC.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IMealService, MealService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IBodyIndicatorService, BodyIndicatorService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
