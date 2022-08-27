using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.DataAccess.Contexts;
using AktifBank.CustomerOrder.Shared.Const;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.ValidationRules
{
    public class DeleteOrderItemReqValidator : AbstractValidator<DeleteOrderItemReq>
    {
        public DeleteOrderItemReqValidator(ProjectDbContext dataContext)
        {
            RuleFor(x => x.CustomerOrderItemId).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "Order Item Id"));

            RuleFor(x => x).Must(x =>
            {
                return dataContext.CustomerOrderDetails.AnyAsync(a => a.Id == x.CustomerOrderItemId, CancellationToken.None).Result;
            }).WithMessage(string.Format(MessageConst.RECORD_NOT_FOUND, "Order Item"));
        }
    }
}
