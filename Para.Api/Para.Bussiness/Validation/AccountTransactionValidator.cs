using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class AccountTransactionValidator  : AbstractValidator<AccountTransactionRequest>
{
    public AccountTransactionValidator()
    {
        
    }
}