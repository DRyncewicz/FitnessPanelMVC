﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Meal;
using FitnessPanelMVC.Application.ViewModels.MealProduct;
using FitnessPanelMVC.Application.ViewModels.Product;
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
        private readonly IMealProductRepository _mealProductRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public MealService(IMealRepository mealRepository, IMapper mapper, IMealProductRepository mealProductRepository, IProductRepository productRepository)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
            _mealProductRepository = mealProductRepository;
            _productRepository = productRepository;

        }

        public ListMealForListVm GetMealsForListByDate(DateTime date, string userId)
        {
            var mealsVm = _mealRepository.GetAllMeals().
                Where(i => i.MealDate.Date == date.Date && i.UserId == userId).
                ProjectTo<MealForListVm>(_mapper.ConfigurationProvider)
                .ToList();

            return new ListMealForListVm
            {
                Meals = mealsVm,
                Count = mealsVm.Count()
            };
        }

        public int AddNewMeal(NewMealVm newMealVm, string userId)
        {

            var meal = _mapper.Map<Meal>(newMealVm);
            meal.UserId = userId;
            int mealId = _mealRepository.CreateMeal(meal);

            return mealId;
        }
        public int AddProductToMeal(int productId, int mealId, double weight)
        {
            var product = _productRepository.GetProductById(productId);
            if (_mealProductRepository.GetAllMealProducts().Any(e => e.ProductId == productId && e.MealId == mealId))
            {
                var mealProduct = _mealProductRepository.GetAllMealProducts().First(e => e.ProductId == productId && e.MealId == mealId);
                mealProduct.Weight += Math.Round(weight, 2);
                mealProduct.Calories += Math.Round(product.CaloriesPer100g * weight / 100, 2);
                mealProduct.Fat += Math.Round(product.FatPer100g * weight / 100, 2);
                mealProduct.Carbs += Math.Round(product.CarbsPer100g * weight / 100, 2);
                mealProduct.Protein += Math.Round(product.ProteinPer100g * weight / 100, 2);

                _mealProductRepository.UpdateMealProduct(mealProduct);
            }
            else
            {
                var mealProduct = new MealProduct
                {
                    ProductId = product.Id,
                    MealId = mealId,
                    Weight = Math.Round(weight, 2),
                    Calories = Math.Round(product.CaloriesPer100g * weight / 100, 2),
                    Fat = Math.Round(product.FatPer100g * weight / 100, 2),
                    Carbs = Math.Round(product.CarbsPer100g * weight / 100, 2),
                    Protein = Math.Round(product.ProteinPer100g * weight / 100, 2),
                };

                _mealProductRepository.CreateMealProduct(mealProduct);
            }

            UpdateMealInformationsAfterProductChange(mealId);
            return mealId;
        }
        public MealForListVm GetMealDetailsById(int mealId)
        {
            var meal = _mealRepository.GetAllMeals().
                FirstOrDefault(m => m.Id == mealId);
            var mealForListVm = _mapper.Map<MealForListVm>(meal);

            return mealForListVm;
        }

        public void DeleteMealById(int mealId)
        {
            _mealRepository.DeleteMeal(mealId);
        }

        public void DeleteProductFromMealById(int productId, int mealId)
        {
            _mealProductRepository.DeleteMealProduct(productId, mealId);
            UpdateMealInformationsAfterProductChange(mealId);
        }


        private void UpdateMealInformationsAfterProductChange(int mealId)
        {

            var meal = _mealRepository.GetAllMeals()
                          .FirstOrDefault(m => m.Id == mealId);
            var mealProducts = meal.MealProducts.ToList();
            meal.TotalCalories = mealProducts
                .Select(m => m.Calories).Sum();
            meal.TotalFat = mealProducts
                .Select(m => m.Fat).Sum();
            meal.TotalCarbs = mealProducts
                .Select(m => m.Carbs).Sum();
            meal.TotalProtein = mealProducts
                .Select(m => m.Protein).Sum();

            _mealRepository.UpdateMeal(meal);
        }
    }
}
