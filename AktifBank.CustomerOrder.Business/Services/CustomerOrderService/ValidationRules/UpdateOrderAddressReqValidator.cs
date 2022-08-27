using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.Shared.Const;
using FluentValidation;

namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.ValidationRules
{
    public class UpdateOrderAddressReqValidator : AbstractValidator<UpdateOrderAddressReq>
    {
        public UpdateOrderAddressReqValidator()
        {
            RuleFor(x => x.CustomerOrderId).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "Order Id"));
            RuleFor(x => x.Address).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "Address"));
        }
    }
}
