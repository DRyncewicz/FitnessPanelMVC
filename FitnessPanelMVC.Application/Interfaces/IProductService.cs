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
        int AddNewProduct(NewProductVm newProductVm, string userId);
        void DeleteProduct(int productId);
        public ListProductForListVm GetAllProductsForList(int pageSize, int pageNo, string searchString, string userId);
        ProductDetailsVm GetProductDetailsById(int productId);
        NewProductVm GetProductForEdit(int productId);
        void UpdateProduct(NewProductVm editedProduct);
    }
}
