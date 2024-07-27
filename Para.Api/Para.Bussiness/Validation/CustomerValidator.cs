using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.IdentityNumber).NotEmpty().MinimumLength(10).MaximumLength(11);
        RuleFor(x => x.Email).EmailAddress().NotEmpty().MinimumLength(5).MaximumLength(100);
        RuleFor(x => x.DateOfBirth).NotNull();

        RuleForEach(x => x.CustomerPhones).SetValidator(new CustomerPhoneValidator());
        RuleForEach(x => x.CustomerAddresses).SetValidator(new CustomerAddressValidator());
        RuleFor(x => x.CustomerDetail).SetValidator(new CustomerDetailValidator());
    }
}