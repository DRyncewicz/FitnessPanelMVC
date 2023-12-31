﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Application.ViewModels.Recipe;
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
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IProductRepository _productRepository;

        private readonly IRecipeProductRepository _recipeProductRepository;

        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, IProductRepository productRepository, IRecipeProductRepository recipeProductRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _productRepository = productRepository;
            _recipeProductRepository = recipeProductRepository;
            _mapper = mapper;
        }

        public async Task<ListRecipeForListVm> GetForListAsync(int pageSize, int pageNo, string searchString, string userId)
        {
            var recipesVm = await _recipeRepository.GetAll().Where(p => p.Name.Contains(searchString) && p.UserId == userId)
                .ProjectTo<RecipeForListVm>(_mapper.ConfigurationProvider)
                .Skip(pageSize * (pageNo - 1)).Take(pageSize).ToListAsync();
            var listRecipesVm = new ListRecipeForListVm()
            {
                PageSize = pageSize,
                PageNo = pageNo,
                SearchString = searchString,
                Recipes = recipesVm,
                Count = recipesVm.Count()
            };

            return listRecipesVm;
        }

        public async Task DeleteAsync(int recipeId)
        {
            await _recipeRepository.DeleteAsync(recipeId);
        }

        public async Task<int> AddNewAsync(NewRecipeVm newRecipeVm, string userId)
        {
            var recipe = _mapper.Map<Recipe>(newRecipeVm);
            recipe.UserId = userId;
            int id = await _recipeRepository.CreateAsync(recipe);
            Product product = new Product()
            {
                Name = newRecipeVm.Name,
                IsConfirmed = false,
                IsUserAdded = true,
                UserId = userId,
            };
            await _productRepository.CreateAsync(product);
            return id;
        }

        public async Task<int> AddProductToRecipeAsync(int productId, int recipeId, double weight)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (_recipeProductRepository.GetAll().Any(e => e.ProductId == productId && e.RecipeId == recipeId))
            {
                var recipeProduct = await _recipeProductRepository.GetAll()
                    .FirstAsync(e => e.ProductId == productId && e.RecipeId == recipeId);
                recipeProduct.Weight += Math.Round(weight, 2);
                recipeProduct.Calories += Math.Round(product.CaloriesPer100g * weight / 100, 2);
                recipeProduct.Fat += Math.Round(product.FatPer100g * weight / 100, 2);
                recipeProduct.Carbs += Math.Round(product.CarbsPer100g * weight / 100, 2);
                recipeProduct.Protein += Math.Round(product.ProteinPer100g * weight / 100, 2);

                await _recipeProductRepository.UpdateAsnyc(recipeProduct);
            }
            else
            {
                var recipeProduct = new RecipeProduct
                {
                    ProductId = product.Id,
                    RecipeId = recipeId,
                    Weight = Math.Round(weight, 2),
                    Calories = Math.Round(product.CaloriesPer100g * weight / 100, 2),
                    Fat = Math.Round(product.FatPer100g * weight / 100, 2),
                    Carbs = Math.Round(product.CarbsPer100g * weight / 100, 2),
                    Protein = Math.Round(product.ProteinPer100g * weight / 100, 2),
                };

                await _recipeProductRepository.CreateAsync(recipeProduct);
            }

            await UpdateRecipeInformationsAfterProductChangeAsync(recipeId);
            return recipeId;
        }

        private async Task UpdateRecipeInformationsAfterProductChangeAsync(int recipeId)
        {

            var recipe = await _recipeRepository.GetByIdAsync(recipeId);
            var recipeProducts = recipe.RecipeProducts.ToList();
            recipe.TotalCalories = recipeProducts
                .Select(m => m.Calories).Sum();
            recipe.TotalFat = recipeProducts
                .Select(m => m.Fat).Sum();
            recipe.TotalCarbs = recipeProducts
                .Select(m => m.Carbs).Sum();
            recipe.TotalProtein = recipeProducts
                .Select(m => m.Protein).Sum();

            await _recipeRepository.UpdateAsync(recipe);
        }

        public async Task<NewProductVm> UpdateProductOnRecipeChangeAsync(int recipeId)
        {
            var recipe = await _recipeRepository.GetByIdAsync(recipeId);
            var totalRecipeWeight = recipe.RecipeProducts.Select(m => m.Weight).Sum();
            var product = _productRepository.GetAll().First(m => m.Name == recipe.Name);
            var newProductVm = _mapper.Map<NewProductVm>(product);
            newProductVm.CaloriesPer100g = Math.Round(recipe.TotalCalories / totalRecipeWeight * 100, 2);
            newProductVm.CarbsPer100g = Math.Round(recipe.TotalCarbs / totalRecipeWeight * 100, 2);
            newProductVm.FatPer100g = Math.Round(recipe.TotalFat / totalRecipeWeight * 100, 2);
            newProductVm.ProteinPer100g = Math.Round(recipe.TotalProtein / totalRecipeWeight * 100, 2);

            return newProductVm;
        }

        public async Task<RecipeForListVm> GetDetailsByIdAsync(int recipeId)
        {
            var recipe = await _recipeRepository.GetByIdAsync(recipeId);
            var recipeVm = _mapper.Map<RecipeForListVm>(recipe);

            return recipeVm;
        }

        public async Task DeleteProductFromMealByIdsAsync(int productId, int recipeId)
        {
            await _recipeProductRepository.DeleteAsync(productId, recipeId);
            await UpdateRecipeInformationsAfterProductChangeAsync(recipeId);
            await UpdateProductOnRecipeChangeAsync(recipeId);
        }
    }
}
