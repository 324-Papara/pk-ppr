using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;

public class CustomerDetailCommandHandler :
    IRequestHandler<CreateCustomerDetailCommand, ApiResponse<CustomerDetailResponse>>,
    IRequestHandler<UpdateCustomerDetailCommand, ApiResponse>,
    IRequestHandler<DeleteCustomerDetailCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CustomerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CustomerDetailResponse>> Handle(CreateCustomerDetailCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
        await unitOfWork.CustomerDetailRepository.Insert(mapped);
        await unitOfWork.Complete();

        var response = mapper.Map<CustomerDetailResponse>(mapped);
        return new ApiResponse<CustomerDetailResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCustomerDetailCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CustomerDetailRequest, CustomerDetail>(request.Request);
        mapped.Id = request.CustomerDetailId;
        unitOfWork.CustomerDetailRepository.Update(mapped);
        await unitOfWork.Complete();
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCustomerDetailCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CustomerDetailRepository.Delete(request.CustomerDetailId);
        await unitOfWork.Complete();
        return new ApiResponse();
    }
}