using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        #region constractor
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region get details
        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                ShortDescription = x.ShortDescription,
                UnitPrice = x.UnitPrice,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,

            }).FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region search
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            #region query
            var query = _context.Products.Include(x => x.Category)
                    .Select(x => new ProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code,
                        Picture = x.Picture,
                        UnitPrice = x.UnitPrice,
                        CategoryName = x.Category.Name,
                        CategoryId = x.CategoryId,
                        CreationDate = x.CreationDate.ToFarsi(),
                        IsInStock = x.IsInStock
                    });
            #endregion

            #region conditions
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if(!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            #endregion

            return query.OrderByDescending(x => x.Id).ToList();

        }
        #endregion

        #region get products
        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel 
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
        #endregion

    }
}
