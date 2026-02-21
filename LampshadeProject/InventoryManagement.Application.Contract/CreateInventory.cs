using System.Security.Cryptography;

namespace InventoryManagement.Application.Contract
{
    public class CreateInventory
    {
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
    }
}
