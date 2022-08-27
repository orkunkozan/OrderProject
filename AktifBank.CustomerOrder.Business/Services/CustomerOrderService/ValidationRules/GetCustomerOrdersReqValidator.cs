using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.Shared.Const;
using FluentValidation;

namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.ValidationRules
{
    public class GetCustomerOrdersReqValidator : AbstractValidator<GetCustomerOrdersReq>
    {
        public GetCustomerOrdersReqValidator()
        {
            RuleFor(x => x.EMail).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "E-Mail"));
            RuleFor(x => x.EMail).EmailAddress().WithMessage(MessageConst.NOT_VALID_EPOSTA);
        }
    }
}
