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
        int AddNew(NewProductVm newProductVm, string userId);

        void DeleteById(int productId);

        public ListProductForListVm GetAllForList(int pageSize, int pageNo, string searchString, string userId);

        ProductDetailsVm GetDetailsById(int productId);

        NewProductVm GetForEdit(int productId);

        void Update(NewProductVm editedProduct);
    }
}
