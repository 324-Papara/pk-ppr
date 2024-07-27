using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerDetailValidator  : AbstractValidator<CustomerDetailRequest>
{
    public CustomerDetailValidator()
    {
        RuleFor(x => x.FatherName).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.MotherName).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.Occupation).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.EducationStatus).NotEmpty().MinimumLength(3).MaximumLength(50);
        RuleFor(x => x.MontlyIncome).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}