﻿using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class RecipeProductRepository : IRecipeProductRepository
    {
        private readonly DbContext _dbCotenxt;

        public RecipeProductRepository(DbContext dbContext)
        {
            _dbCotenxt = dbContext;
        }

        public int CreateRecipeProduct(RecipeProduct recipeProduct)
        {
            _dbCotenxt.RecipeProduct.Add(recipeProduct);
            _dbCotenxt.SaveChanges();
            return recipeProduct.Id;
        }

        public void DeleteRecipeProduct (int id)
        {
            var recipeProduct = _dbCotenxt.RecipeProduct.Find(id);
            if (recipeProduct != null)
            {
                _dbCotenxt.RecipeProduct.Remove(recipeProduct);
                _dbCotenxt.SaveChanges();
            }
        }

        public int UpdateRecipeProduct (RecipeProduct recipeProduct)
        {
            if (_dbCotenxt.RecipeProduct.Find(recipeProduct.Id) != null)
            {
                _dbCotenxt.RecipeProduct.Update(recipeProduct);
                _dbCotenxt.SaveChanges();
                return recipeProduct.Id;
            }
            return 0;
        }
        public IQueryable<RecipeProduct> GetAllRecipeProducts()
        {
            var recipeProducts = _dbCotenxt.RecipeProduct;
            return recipeProducts.AsQueryable(); ;
        }
    }
}
