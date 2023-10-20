using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class MealProductRepository : IMealProductRepository<MealProduct>
    {
        private readonly Context _dbCotenxt;
        public MealProductRepository(Context dbContext)
        {
            _dbCotenxt = dbContext;
        }

        public int CreateMealProduct(MealProduct mealProduct)
        {
            _dbCotenxt.MealProduct.Add(mealProduct);
            _dbCotenxt.SaveChanges();
            return mealProduct.Id;
        }
        public void DeleteMealProduct(int id)
        {
            var mealProduct = _dbCotenxt.MealProduct.Find(id);
            if (mealProduct != null)
            {
                _dbCotenxt.MealProduct.Remove(mealProduct);
                _dbCotenxt.SaveChanges();
            }
        }
        public int UpdateMealProduct(MealProduct mealProduct)
        {
            if (_dbCotenxt.MealProduct.Find(mealProduct.Id) != null)
            {
                _dbCotenxt.MealProduct.Update(mealProduct);
                _dbCotenxt.SaveChanges();
                return mealProduct.Id;
            }
            return 0;
        }
        public IQueryable<MealProduct> GetMealProductByMealId(int mealId)
        {
            var mealProducts = _dbCotenxt.MealProduct.Where(x => x.MealId == mealId);
            return mealProducts;
        }
        
    }
}
