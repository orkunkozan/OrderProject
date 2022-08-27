namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models;

public class OrderItemCreateDto
{
    public string Barcode { get; set; } = string.Empty;
    public string? Explanation { get; set; } = string.Empty;
    public int Piece { get; set; }
    public decimal Amount { get; set; }
}