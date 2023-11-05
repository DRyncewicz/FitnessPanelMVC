using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Meal;
using FitnessPanelMVC.Application.ViewModels.MealProduct;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
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

        public ListMealForListVm GetForListByDate(DateTime date, string userId)
        {
            var mealsVm = _mealRepository.GetAll().
                Where(i => i.MealDate.Date == date.Date && i.UserId == userId).
                ProjectTo<MealForListVm>(_mapper.ConfigurationProvider).ToList();

            return new ListMealForListVm
            {
                Meals = mealsVm,
                Count = mealsVm.Count()
            };
        }

        public async Task<int> AddNewAsync(NewMealVm newMealVm, string userId)
        {

            var meal = _mapper.Map<Meal>(newMealVm);
            meal.UserId = userId;
            int mealId = await _mealRepository.CreateAsync(meal);

            return mealId;
        }

        public async Task<int> AddProductToMealAsync(int productId, int mealId, double weight)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (_mealProductRepository.GetAll()
                .Any(e => e.ProductId == productId && e.MealId == mealId))
            {
                var mealProduct = await _mealProductRepository.GetAll()
                    .FirstAsync(e => e.ProductId == productId && e.MealId == mealId);
                mealProduct.Weight += Math.Round(weight, 2);
                mealProduct.Calories += Math.Round(product.CaloriesPer100g * weight / 100, 2);
                mealProduct.Fat += Math.Round(product.FatPer100g * weight / 100, 2);
                mealProduct.Carbs += Math.Round(product.CarbsPer100g * weight / 100, 2);
                mealProduct.Protein += Math.Round(product.ProteinPer100g * weight / 100, 2);

                await _mealProductRepository.UpdateAsync(mealProduct);
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

                await _mealProductRepository.CreateAsync(mealProduct);
            }

            await UpdateMealInformationsAfterProductChangeAsync(mealId);
            return mealId;
        }

        public async Task<MealForListVm> GetDetailsByIdAsync(int mealId)
        {
            var meal = await _mealRepository.GetByIdAsync(mealId);
            var mealForListVm = _mapper.Map<MealForListVm>(meal);

            return mealForListVm;
        }

        public async Task DeleteByIdAsync(int mealId)
        {
            await _mealRepository.DeleteAsync(mealId);
        }

        public async Task DeleteProductFromMealByIdAsync(int productId, int mealId)
        {
            await _mealProductRepository.DeleteAsync(productId, mealId);
            await UpdateMealInformationsAfterProductChangeAsync(mealId);
        }

        private async Task UpdateMealInformationsAfterProductChangeAsync(int mealId)
        {
            var meal = await _mealRepository.GetByIdAsync(mealId);
            var mealProducts = meal.MealProducts.ToList();
            meal.TotalCalories = mealProducts
                .Select(m => m.Calories).Sum();
            meal.TotalFat = mealProducts
                .Select(m => m.Fat).Sum();
            meal.TotalCarbs = mealProducts
                .Select(m => m.Carbs).Sum();
            meal.TotalProtein = mealProducts
                .Select(m => m.Protein).Sum();

            await _mealRepository.UpdateAsync(meal);
        }
    }
}
