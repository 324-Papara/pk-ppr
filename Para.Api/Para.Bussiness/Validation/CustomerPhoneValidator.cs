using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerPhoneValidator  : AbstractValidator<CustomerPhoneRequest>
{
    public CustomerPhoneValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.CountyCode).NotEmpty().MinimumLength(3).MaximumLength(3);
        RuleFor(x => x.Phone).NotEmpty().MinimumLength(10).MaximumLength(10);
    }
}