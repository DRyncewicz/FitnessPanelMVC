using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPanelMVC.Application.Interfaces;
using FitnessPanelMVC.Application.ViewModels.Recipe;
using FitnessPanelMVC.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRecipeProductRepository _recipeProductRepository;
        private readonly IMapper _mapper;
        public RecipeService(IRecipeRepository recipeRepository, IProductRepository productRepository, IRecipeProductRepository recipeProductRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _productRepository = productRepository;
            _recipeProductRepository = recipeProductRepository;
            _mapper = mapper;
        }

        public ListRecipeForListVm GetRecipesForList(int pageSize, int pageNo, string searchString)
        {
            var recipesVm = _recipeRepository.GetAllRecipes().Where(p => p.Name.Contains(searchString))
                .ProjectTo<RecipeForListVm>(_mapper.ConfigurationProvider)
                .Skip(pageSize * (pageNo -1)).Take(pageSize).ToList();
            var listRecipesVm = new ListRecipeForListVm()
            {
                PageSize = pageSize,
                PageNo = pageNo,
                SearchString = searchString,
                Recipes = recipesVm,
                Count = recipesVm.Count()
            };

            return listRecipesVm;
        }
    }
}
