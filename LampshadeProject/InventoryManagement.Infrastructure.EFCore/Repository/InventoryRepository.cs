using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        #region constractor
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;
        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
        {
            _inventoryContext = inventoryContext;
            _shopContext = shopContext;
        }
        #endregion

        #region get by productId
        public Inventory GetByProductId(long productId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }
        #endregion

        #region getDetails
        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }
        #endregion

        #region search
        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name });
            #region query
            var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                InStock = x.InStock,
                CurrentCount = x.CalculateCurrentCount()
            });
            #endregion

            #region conditions
            if(searchModel.ProductId > 0 )
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!searchModel.InStock)
                query = query.Where(x => !x.InStock);
            #endregion

            var inventory = query.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach(item =>
                item.ProductName = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }
        #endregion
    }
}
