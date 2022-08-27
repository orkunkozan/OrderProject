using AktifBank.CustomerOrder.Business.Helper;
using AktifBank.CustomerOrder.Business.Services.Abstract;
using AktifBank.CustomerOrder.Business.Services.Base;
using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models;
using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Response;
using AktifBank.CustomerOrder.DataAccess.Contexts;
using AktifBank.CustomerOrder.Shared.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService
{
    public class CustomerOrderService : BaseService, ICustomerOrderService
    {
        private IValidator<GetCustomerOrdersReq> _validatorGetCustomerOrders;
        private IValidator<CreateNewOrderReq> _validatorCreateNewOrder;
        private IValidator<DeleteOrderItemReq> _validatorDeleteOrderItem;
        private IValidator<DeleteOrderReq> _validatorDeleteOrder;
        private IValidator<GetOrderReq> _validatorGetOrder;
        private IValidator<UpdateOrderAddressReq> _validatorUpdateOrderAddress;
        private IValidator<UpdateOrderReq> _validatorUpdateOrder;

        //Constract da bu kadar fazla inject yapmak iyi değil ancak Mediator kullanmadığım için bu işi otomatize etmedim manuel yapıyorum.
        public CustomerOrderService(ProjectDbContext dataContext, IValidator<GetCustomerOrdersReq> validatorGetCustomerOrders,
            IValidator<CreateNewOrderReq> validatorCreateNewOrder, IValidator<DeleteOrderItemReq> validatorDeleteOrderItem, IValidator<DeleteOrderReq> validatorDeleteOrder,
            IValidator<GetOrderReq> validatorGetOrder, IValidator<UpdateOrderAddressReq> validatorUpdateOrderAddress, IValidator<UpdateOrderReq> validatorUpdateOrder) : base(dataContext)
        {
            _validatorGetCustomerOrders = validatorGetCustomerOrders;
            _validatorCreateNewOrder = validatorCreateNewOrder;
            _validatorDeleteOrderItem = validatorDeleteOrderItem;
            _validatorDeleteOrder = validatorDeleteOrder;
            _validatorGetOrder = validatorGetOrder;
            _validatorUpdateOrderAddress = validatorUpdateOrderAddress;
            _validatorUpdateOrder = validatorUpdateOrder;
        }

        public async Task<List<GetCustomerOrdersRes>> GetCustomerOrdersAsync(GetCustomerOrdersReq request)
        {
            _validatorGetCustomerOrders.ValidateAsync(request, CancellationToken.None).Result.IsNotValid();

            var result = await _dataContext.CustomerOrders.Include(i => i.CustomerOrderDetails)
                  .Where(w => _dataContext.Customers.Any(a => a.EMail == request.EMail && a.Id == w.CustomerId))
                  .Select(s => new GetCustomerOrdersRes
                  {
                      OrderId = s.Id,
                      OrderDate = s.AddTime,
                      OrderAddress = s.Address,
                      TotalAmount = s.CustomerOrderDetails.Sum(s => s.Piece * s.Amount),
                      ItemCount = s.CustomerOrderDetails.Count()
                  }).ToListAsync(CancellationToken.None);
            return result;
        }

        public async Task<GetOrderRes> GetOrderAsync(GetOrderReq request)
        {
            _validatorGetOrder.ValidateAsync(request, CancellationToken.None).Result.IsNotValid();
            var result = await _dataContext.CustomerOrders.Include(i => i.CustomerOrderDetails)
                 .Where(w => w.Id == request.CustomerOrderId)
                 .Select(s => new GetOrderRes
                 {
                     OrderId = s.Id,
                     OrderAddress = s.Address,
                     OrderDate = s.AddTime,
                     OrderItems = s.CustomerOrderDetails.Select(ss => new OrderItemDto
                     {
                         Amount = ss.Amount,
                         Barcode = ss.Barcode,
                         Piece = ss.Piece,
                         Explanation = ss.Explanation,
                         Id = ss.Id
                     }).ToList()
                 }).FirstOrDefaultAsync(CancellationToken.None) ?? new();
            return result;
        }

        public async Task CreateNewOrderAsync(CreateNewOrderReq request)
        {
            _validatorCreateNewOrder.ValidateAsync(request, CancellationToken.None).Result.IsNotValid();
            await _dataContext.Database.BeginTransactionAsync(CancellationToken.None);
            try
            {
                var customerId = await CreateOrUpdateCustomerAsync(request);
                var customerOrder = new Shared.Entities.CustomerOrder
                {
                    CustomerId = customerId,
                    Address = request.OrderAddress
                };
                await _dataContext.CustomerOrders.AddAsync(customerOrder, CancellationToken.None);
                await _dataContext.SaveChangesAsync(CancellationToken.None);
                await CreateOrderDetailRangeAsync(customerOrder.Id, request.Orders);
                await _dataContext.Database.CommitTransactionAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                await _dataContext.Database.RollbackTransactionAsync(CancellationToken.None);
                throw;
            }
        }

        public async Task UpdateOrderAsync(UpdateOrderReq request)
        {
            _validatorUpdateOrder.ValidateAsync(request, CancellationToken.None).Result.IsNotValid();
            try
            {
                await _dataContext.Database.BeginTransactionAsync(CancellationToken.None);
                if (request.CreatedOrderItems.Any())
                {
                    await CreateOrderDetailRangeAsync(request.CustomerOrderId, request.CreatedOrderItems);
                }

                if (request.UpdatedOrderItems.Any())
                {
                    await UpdateOrderDetailRangeAsync(request.UpdatedOrderItems);
                }

                if (request.DeletedOrderItem.Any())
                {
                    await DeleteOrderDetailRangeAsync(request.DeletedOrderItem);
                }
                await _dataContext.Database.CommitTransactionAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                await _dataContext.Database.RollbackTransactionAsync(CancellationToken.None);
                throw;
            }

        }

        public async Task UpdateOrderAddressAsync(UpdateOrderAddressReq request)
        {
            _validatorUpdateOrderAddress.ValidateAsync(request, CancellationToken.None).Result.IsNotValid();
            var record = await _dataContext.CustomerOrders.FirstOrDefaultAsync(w => w.Id == request.CustomerOrderId, CancellationToken.None);
            record.Address = request.Address;
            await _dataContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteOrderAsync(DeleteOrderReq request)
        {
            _validatorDeleteOrder.ValidateAsync(request, CancellationToken.None).Result.IsNotValid();
            var deleteOrderRecord = await _dataContext.CustomerOrders.FirstOrDefaultAsync(w => w.Id == request.CustomerOrderId, CancellationToken.None);
            _dataContext.Remove(deleteOrderRecord);
            await _dataContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteOrderItemAsync(DeleteOrderItemReq request)
        {
            _validatorDeleteOrderItem.ValidateAsync(request, CancellationToken.None).Result.IsNotValid();
            var deleteOrderItemRecord = await _dataContext.CustomerOrderDetails.FirstOrDefaultAsync(w => w.Id == request.CustomerOrderItemId, CancellationToken.None);
            _dataContext.Remove(deleteOrderItemRecord);
            await _dataContext.SaveChangesAsync(CancellationToken.None);
        }






        private async Task<int> CreateOrUpdateCustomerAsync(CreateNewOrderReq request)
        {
            var customer =
                await _dataContext.Customers.FirstOrDefaultAsync(w => w.EMail == request.CustomerEMail, CancellationToken.None) ?? new()
                {
                    EMail = request.CustomerEMail,
                };

            customer.Name = request.CustomerName;
            if (customer.Id == 0)
            {
                await _dataContext.AddAsync(customer, CancellationToken.None);
            }

            await _dataContext.SaveChangesAsync(CancellationToken.None);
            return customer.Id;
        }

        private async Task CreateOrderDetailRangeAsync(int customerOrderId, List<OrderItemCreateDto> orderDetails)
        {
            var addingOrderDetailRecord = orderDetails.Select(s => new CustomerOrderDetail
            {
                Amount = s.Amount,
                Barcode = s.Barcode,
                Explanation = s.Explanation,
                CustomerOrderId = customerOrderId,
                Piece = s.Piece
            }).ToList();
            await _dataContext.CustomerOrderDetails.AddRangeAsync(addingOrderDetailRecord, CancellationToken.None);
            await _dataContext.SaveChangesAsync(CancellationToken.None);
        }

        private async Task UpdateOrderDetailRangeAsync(List<OrderItemUpdateDto> orderDetails)
        {
            foreach (var item in orderDetails)
            {
                var updateRecord = await _dataContext.CustomerOrderDetails.FirstOrDefaultAsync(w => w.Id == item.Id, CancellationToken.None);
                updateRecord.Barcode = item.Barcode;
                updateRecord.Amount = item.Amount;
                updateRecord.Explanation = item.Explanation;
                updateRecord.Piece = item.Piece;
            }
            await _dataContext.SaveChangesAsync(CancellationToken.None);
        }

        private async Task DeleteOrderDetailRangeAsync(List<int> orderDetails)
        {
            var deleteRecords = await _dataContext.CustomerOrderDetails.Where(w => orderDetails.Contains(w.Id)).ToListAsync(CancellationToken.None);
            _dataContext.CustomerOrderDetails.RemoveRange(deleteRecords);
            await _dataContext.SaveChangesAsync(CancellationToken.None);
        }

    }
}
