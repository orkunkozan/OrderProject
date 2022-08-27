using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.DataAccess.Contexts;
using AktifBank.CustomerOrder.Shared.Const;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.ValidationRules
{
    public class DeleteOrderReqValidator : AbstractValidator<DeleteOrderReq>
    {
        public DeleteOrderReqValidator(ProjectDbContext dataContext)
        {
            RuleFor(x => x.CustomerOrderId).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "Order Id"));

            RuleFor(x => x).Must(x =>
            {
                return dataContext.CustomerOrders.AnyAsync(a => a.Id == x.CustomerOrderId, CancellationToken.None).Result;
            }).WithMessage(string.Format(MessageConst.RECORD_NOT_FOUND, "Order"));
        }
    }
}
