namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request
{
    public class UpdateOrderReq
    {
        public int CustomerOrderId { get; set; }
        public List<OrderItemCreateDto> CreatedOrderItems { get; set; } = new();
        public List<OrderItemUpdateDto> UpdatedOrderItems { get; set; } = new();
        public List<int> DeletedOrderItem { get; set; } = new();
    }
}
