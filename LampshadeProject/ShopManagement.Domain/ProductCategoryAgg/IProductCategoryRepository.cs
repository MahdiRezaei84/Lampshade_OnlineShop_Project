using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory.ViewModels;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long, ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategoryViewModel GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
