namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request
{
    public class CreateNewOrderReq
    {
        public string CustomerEMail { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string OrderAddress { get; set; } = string.Empty; 

        public List<OrderItemCreateDto> Orders { get; set; } = new();
    }
}
