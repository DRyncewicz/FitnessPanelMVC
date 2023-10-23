using FitnessPanelMVC.Domain.Interface;
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

        public int CreateMealProduct(MealProduct mealProduct)
        {
            _dbContext.MealProduct.Add(mealProduct);
            _dbContext.SaveChanges();
            return mealProduct.Id;
        }
        public void DeleteMealProduct(int id)
        {
            var mealProduct = _dbContext.MealProduct.Find(id);
            if (mealProduct != null)
            {
                _dbContext.MealProduct.Remove(mealProduct);
                _dbContext.SaveChanges();
            }
        }
        public int UpdateMealProduct(MealProduct mealProduct)
        {
            if (_dbContext.MealProduct.Find(mealProduct.Id) != null)
            {
                _dbContext.MealProduct.Update(mealProduct);
                _dbContext.SaveChanges();
                return mealProduct.Id;
            }
            return 0;
        }
        public IQueryable<MealProduct> GetAllMealProduct()
        {
            var mealProducts =_dbContext.MealProduct;
            
            return mealProducts;
        }
        
    }
}
