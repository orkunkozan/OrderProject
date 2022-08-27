namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request
{
    public class UpdateOrderAddressReq
    {
        public int CustomerOrderId { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
