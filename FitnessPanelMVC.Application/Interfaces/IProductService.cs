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
        Task<int> AddNewAsync(NewProductVm newProductVm, string userId);
        Task AddFromXmlFileAsync(string filePath, string userId);
        Task DeleteByIdAsync(int productId);

        Task<ListProductForListVm> GetAllForListAsync(int pageSize, int pageNo, string searchString, string userId);

        Task<ProductDetailsVm> GetDetailsByIdAsync(int productId);

        Task<NewProductVm> GetForEditAsync(int productId);

        Task UpdateAsync(NewProductVm editedProduct);
    }
}
