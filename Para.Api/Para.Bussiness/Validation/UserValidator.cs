using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class UserValidator  : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().GreaterThan(0);
    }
}