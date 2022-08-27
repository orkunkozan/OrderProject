namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Response
{
    public class GetCustomerOrdersRes
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } 
        public string OrderAddress { get; set; } = string.Empty;
        public int ItemCount { get; set; }
        public decimal TotalAmount { get; set; } 
    }
}
