using FitnessPanelMVC.Application.ViewModels.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IRecipeService
    {
        ListRecipeForListVm GetRecipesForList(int pageSize, int pageNo, string searchString);
    }
}
