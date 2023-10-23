using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Meal;
using FitnessPanelMVC.Application.ViewModels.MealProduct;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;

        private readonly IMapper _mapper;

        public MealService(IMealRepository mealRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public ListMealForListVm GetMealsForListByDate(DateTime date)
        {
            var meals = _mealRepository.GetAllMeals().Where(i => i.MealDate.Date == date.Date);
            var mealProducts = meals.SelectMany(m => m.MealProducts).AsQueryable();
            var products = mealProducts.Select(m => m.Product).AsQueryable();

            var mealsVm = meals.ProjectTo<MealForListVm>(_mapper.ConfigurationProvider).ToList();
            var mealProductsVm = mealProducts.ProjectTo<MealProductForListVm>(_mapper.ConfigurationProvider).ToList();

            return new ListMealForListVm
            {
                Meals = mealsVm,
                Count = meals.Count()
            };
        }
    }
}
