using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerAddressValidator  : AbstractValidator<CustomerAddressRequest>
{
    public CustomerAddressValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Country).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.City).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.AddressLine).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.ZipCode).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}