using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeProduct> RecipeProducts { get; set; }
        public double TotalCalories { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFat { get; set; }
        public double TotalProtein { get; set; }
    }
}
