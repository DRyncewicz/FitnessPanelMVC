using FitnessPanelMVC.Domain.Model.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Recipe : BaseEntity
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<RecipeProduct> RecipeProducts { get; set; }

        public double TotalCalories { get; set; }

        public double TotalCarbs { get; set; }

        public double TotalFat { get; set; }

        public double TotalProtein { get; set; }
    }
}
