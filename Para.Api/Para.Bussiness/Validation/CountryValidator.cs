using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CountryValidator  : AbstractValidator<CountryRequest>
{
    public CountryValidator()
    {
        RuleFor(x => x.CountyCode).NotEmpty().MinimumLength(3).MaximumLength(3);
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(50);
    }
}