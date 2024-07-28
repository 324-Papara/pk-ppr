using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;

public class AccountCommandHandler :
    IRequestHandler<CreateAccountCommand, ApiResponse<AccountResponse>>,
    IRequestHandler<UpdateAccountCommand, ApiResponse>,
    IRequestHandler<DeleteAccountCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public AccountCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<AccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountRequest, Account>(request.Request);
        await unitOfWork.AccountRepository.Insert(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<AccountResponse>(mapped);
        return new ApiResponse<AccountResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<AccountRequest, Account>(request.Request);
        mapped.Id = request.AccountId;
        unitOfWork.AccountRepository.Update(mapped);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.AccountRepository.Delete(request.AccountId);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}