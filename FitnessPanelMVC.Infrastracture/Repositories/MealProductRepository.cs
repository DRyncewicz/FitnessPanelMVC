﻿using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class MealProductRepository : IMealProductRepository
    {
        private readonly Context _dbContext;
        public MealProductRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateMealProduct(MealProduct mealProduct)
        {
            _dbContext.MealProduct.Add(mealProduct);
            _dbContext.SaveChanges();
        }

        public void DeleteMealProduct(int mealId, int productId)
        {
            var mealProduct = _dbContext.MealProduct.Find(mealId, productId);
            if (mealProduct != null)
            {
                _dbContext.MealProduct.Remove(mealProduct);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateMealProduct(MealProduct mealProduct)
        {
            var existingMealProduct = _dbContext.MealProduct
                .Find(mealProduct.MealId, mealProduct.ProductId);

            if (existingMealProduct != null)
            {
                _dbContext.Entry(existingMealProduct).CurrentValues.SetValues(mealProduct);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<MealProduct> GetAllMealProducts()
        {
            return _dbContext.MealProduct;
        }
    }
}
