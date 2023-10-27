using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.Recipe
{
    public class ListRecipeForListVm
    {
        public List<RecipeForListVm> Recipes { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public string SearchString { get; set; }
    }
}
