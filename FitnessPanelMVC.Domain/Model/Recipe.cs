using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RecipeProduct> RecipeProducts { get; set; }
    }
}
