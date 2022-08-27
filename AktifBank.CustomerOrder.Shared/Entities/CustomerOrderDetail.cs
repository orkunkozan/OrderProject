using AktifBank.CustomerOrder.Shared.Entities.Base;

namespace AktifBank.CustomerOrder.Shared.Entities
{
    public class CustomerOrderDetail : BaseEntity
    {
        public int CustomerOrderId { get; set; }  
        public string Barcode { get; set; } = string.Empty;
        public string? Explanation { get; set; } = string.Empty;
        public int Piece { get; set; }
        public decimal Amount { get; set; }

        public CustomerOrder CustomerOrder { get; set; }  
    }
}
