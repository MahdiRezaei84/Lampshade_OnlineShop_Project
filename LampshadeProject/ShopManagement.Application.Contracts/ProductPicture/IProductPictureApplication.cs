using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult CreateProductPicture(CreateProductPicture command);
        OperationResult EditProductPicture(EditProductPicture command);
        OperationResult RemoveProductPicture(long id);
        OperationResult RestoreProductPicture(long id);
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
