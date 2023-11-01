using FitnessPanelMVC.Domain.Model.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Product : BaseEntity
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User {get; set; }

        public string? Restaurant { get; set; }

        public string? Barcode { get; set; }

        public double CaloriesPer100g { get; set; }

        public double CarbsPer100g { get; set; }

        public double FatPer100g { get; set; }

        public double ProteinPer100g { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsUserAdded { get; set; }

        public ICollection<MealProduct> MealProducts { get; set; }

        public ICollection<RecipeProduct> RecipeProduct { get; set; }

    }
}
