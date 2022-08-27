using FluentValidation.Results;

namespace AktifBank.CustomerOrder.Business.Helper
{
    public static class ValidatorHelper
    {
        public static void IsNotValid(this ValidationResult validatorResult)
        {
            if (!validatorResult.IsValid)
            {
                throw new AppException(validatorResult.ToString()); 
            }
        }
    }
}
