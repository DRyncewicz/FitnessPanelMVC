using FitnessPanelMVC.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IProductService
    {
        public ListProductForListVm GetAllProductsForList(int pageSize, int pageNo, string searchString);
    }
}
