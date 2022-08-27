using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.DataAccess.Contexts;
using AktifBank.CustomerOrder.Shared.Const;
using FluentValidation;

namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.ValidationRules
{
    public class CreateNewOrderReqValidator : AbstractValidator<CreateNewOrderReq>
    {
        public CreateNewOrderReqValidator(ProjectDbContext dataContext)
        { 
            RuleFor(x => x.CustomerEMail).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "E-Mail"));
            RuleFor(x => x.CustomerEMail).EmailAddress().WithMessage(MessageConst.NOT_VALID_EPOSTA);
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY,"Name"));
            RuleFor(x => x.OrderAddress).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY,"Address"));
            RuleFor(x => x.Orders).Must(x =>
            {
                return x != null && x.Count > 0;
            }) .WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "Order Item"));
            RuleFor(x => x).Must(x =>
            {
                return !x.Orders.Any(w => w.Piece <= 0);
            }).When(w=> w.Orders.Any()) .WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_PRIECE_LESSTHENEQUAL_ZERO);


            RuleFor(x => x).Must(x =>
            {
                return !x.Orders.Any(w => w.Amount <= 0);
            }).When(w => w.Orders.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_AMOUNT_LESSTHENEQUAL_ZERO);

            RuleFor(x => x).Must(x =>
            {
                return !x.Orders.Any(w => string.IsNullOrEmpty(w.Barcode) );
            }).When(w => w.Orders.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_BARCODE_IS_NULL); 

        }
    }
}
