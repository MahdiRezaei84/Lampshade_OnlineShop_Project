using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        #region constractor
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context) 
        {
            _context = context;
        }
        #endregion

        #region get details
        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture 
            { 
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }
        #endregion

        #region search
        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            #region query
            var query = _context.ProductPictures
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    ProductName = x.Product.Name,
                    CreationDate = x.CreationDate.ToString(),
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemoved,
                });
            #endregion

            #region conditions
            if(searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            #endregion

            return query.OrderByDescending(x => x.Id).ToList();

        }
        #endregion
    }
}
