using FitnessPanelMVC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Meal
    {
        public int Id { get; set; }
        public MealType MealType { get; set; }
        public DateTime MealDate { get; set; }
        public ICollection<MealProduct> MealProducts { get; set; }
    }
}
