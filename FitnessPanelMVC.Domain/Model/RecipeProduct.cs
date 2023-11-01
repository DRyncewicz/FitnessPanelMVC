using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class RecipeProduct
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public double Weight { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Fat { get; set; }

        public double Carbs { get; set; }
    }
}
