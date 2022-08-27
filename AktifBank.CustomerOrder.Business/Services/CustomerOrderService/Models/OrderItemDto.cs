namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models
{
    public class OrderItemDto : OrderItemCreateDto
    {
        public int Id { get; set; }
        public decimal TotalAmount => Piece * Amount;
    }
}
