using AktifBank.CustomerOrder.Business.Services.CustomerOrderService.Models.Request;
using AktifBank.CustomerOrder.DataAccess.Contexts;
using AktifBank.CustomerOrder.Shared.Const;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AktifBank.CustomerOrder.Business.Services.CustomerOrderService.ValidationRules
{
    public class UpdateOrderReqValidator : AbstractValidator<UpdateOrderReq>
    {
        public UpdateOrderReqValidator(ProjectDbContext dataContext)
        {
            RuleFor(x => x.CustomerOrderId).NotEmpty().WithMessage(string.Format(MessageConst.CANNOT_BE_EMPTY, "Order Id"));

            RuleFor(x => x).Must(x =>
            {
                return !x.UpdatedOrderItems.Any(w => w.Amount <= 0);
            }).When(w => w.UpdatedOrderItems.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_AMOUNT_LESSTHENEQUAL_ZERO);

            RuleFor(x => x).Must(x =>
            {
                return !x.UpdatedOrderItems.Any(w => w.Piece <= 0);
            }).When(w => w.UpdatedOrderItems.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_PRIECE_LESSTHENEQUAL_ZERO);

            RuleFor(x => x).Must(x =>
            {
                return !x.UpdatedOrderItems.Any(w => string.IsNullOrEmpty(w.Barcode));
            }).When(w => w.UpdatedOrderItems.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_BARCODE_IS_NULL);


            RuleFor(x => x).Must(x =>
            {
                return !x.CreatedOrderItems.Any(w => string.IsNullOrEmpty(w.Barcode));
            }).When(w => w.CreatedOrderItems.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_BARCODE_IS_NULL);


            RuleFor(x => x).Must(x =>
            {
                return !x.CreatedOrderItems.Any(w => w.Piece <= 0);
            }).When(w => w.CreatedOrderItems.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_PRIECE_LESSTHENEQUAL_ZERO);

            RuleFor(x => x).Must(x =>
            {
                return !x.CreatedOrderItems.Any(w => w.Amount <= 0);
            }).When(w => w.CreatedOrderItems.Any()).WithMessage(MessageConst.THERE_CANNOT_BE_A_RECORD_AMOUNT_LESSTHENEQUAL_ZERO);


            RuleFor(x => x).Must(x =>
            {
                var dbOrderDetail = dataContext.CustomerOrderDetails.Where(w => w.CustomerOrderId == x.CustomerOrderId).Select(s => s.Id).ToListAsync().Result;
                return !x.DeletedOrderItem.Any(a => !dbOrderDetail.Contains(a));
            }).When(w => w.DeletedOrderItem.Any()).WithMessage(MessageConst.NON_RECORD_CANNOT_BE_DELETED);

        }
    }
}
