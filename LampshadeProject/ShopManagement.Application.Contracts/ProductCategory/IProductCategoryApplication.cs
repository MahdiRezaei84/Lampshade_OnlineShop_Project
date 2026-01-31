using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory.ViewModels;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult CreateProductCategory(CreateProductCategoryViewModel command);
        OperationResult EditProductCategory(EditProductCategoryViewModel command);
        EditProductCategoryViewModel GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        List<ProductCategoryViewModel> GetProductCategories();

    }
}
