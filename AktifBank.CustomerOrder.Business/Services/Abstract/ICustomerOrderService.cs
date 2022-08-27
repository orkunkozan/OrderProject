using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Response;

namespace AktifBank.CustomerOrder.Business.Services.Abstract
{
    public interface ICustomerOrderService
    {
        Task<List<GetCustomerOrdersRes>> GetCustomerOrdersAsync(GetCustomerOrdersReq request );
        Task<GetOrderRes> GetOrderAsync(GetOrderReq request);
        Task CreateNewOrderAsync(CreateNewOrderReq request);
        Task UpdateOrderAsync(UpdateOrderReq request);
        Task UpdateOrderAddressAsync(UpdateOrderAddressReq request);
        Task DeleteOrderAsync(DeleteOrderReq request);
        Task DeleteOrderItemAsync(DeleteOrderItemReq request);
    }
}
