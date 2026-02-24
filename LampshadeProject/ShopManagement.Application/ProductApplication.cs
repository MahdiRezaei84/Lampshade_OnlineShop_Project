using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        #region constractor
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region create product
        public OperationResult CreateProduct(CreateProduct command)
        {
            var operation = new OperationResult();

            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DouplicatedRecord);

            var slug = command.Slug.Slugify();
            var product = new Product(command.Name, command.Code, command.ShortDescription, 
                command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return operation.Succedded();

        }
        #endregion

        #region edit product
        public OperationResult EditProduct(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetById(command.Id);

            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DouplicatedRecord);

            var slug = command.Slug.Slugify();
            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);

            _productRepository.SaveChanges();

            return operation.Succedded();
        }
        #endregion

        #region get product details
        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        #endregion

        #region search
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
        #endregion

        #region get products
        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
        #endregion

    }
}
