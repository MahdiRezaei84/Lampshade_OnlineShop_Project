using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult CreateProduct(CreateProduct command);
        OperationResult EditProduct(EditProduct command);
        OperationResult IsStock(long id);
        OperationResult NotInStock(long id);
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
