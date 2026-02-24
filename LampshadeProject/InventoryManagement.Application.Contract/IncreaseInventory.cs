using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contract
{
    public class IncreaseInventory
    {
        public long InventoryId { get; set; }

        [Range(1, 100000, ErrorMessage =ValidationMessages.IsRequired)]
        public long Count { get; set; }
        public string? Description { get; set; }
    }
}
