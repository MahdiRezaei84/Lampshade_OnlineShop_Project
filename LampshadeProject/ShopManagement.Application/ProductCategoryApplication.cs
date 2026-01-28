using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductCategory.ViewModels;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        #region constractor
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        #endregion

        #region create productCategory
        public OperationResult CreateProductCategory(CreateProductCategoryViewModel command)
        {
            var operation = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجدد تلاش نمائید");

            var slug = GenerateSlug.Slugify(command.Slug);
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);

            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();

            return operation.Succedded();
        }
        #endregion

        #region edit productCategory
        public OperationResult EditProductCategory(EditProductCategoryViewModel command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.GetById(command.Id);
            if (productCategory == null)
                return operation.Failed("رکورد با اطلاعات خواسته شده یافت نشد. لطفا مجدد تلاش نمائید");

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد. لطفا مجدد تلاش نمائید");

            var slug = GenerateSlug.Slugify(command.Slug);
            productCategory.Edit(command.Name, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription, slug);

            _productCategoryRepository.SaveChanges();
            return operation.Succedded();
        }
        #endregion

        #region get details
        public EditProductCategoryViewModel GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }
        #endregion

        #region search
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
        #endregion
    }
}
