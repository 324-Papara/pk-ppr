using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class AccountValidator  : AbstractValidator<AccountRequest>
{
    public AccountValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().GreaterThan(0);
    }
}