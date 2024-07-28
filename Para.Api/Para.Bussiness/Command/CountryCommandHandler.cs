using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;

public class CountryCommandHandler :
    IRequestHandler<CreateCountryCommand, ApiResponse<CountryResponse>>,
    IRequestHandler<UpdateCountryCommand, ApiResponse>,
    IRequestHandler<DeleteCountryCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IMemoryCache memoryCache;
    private readonly IDistributedCache distributedCache;

    public CountryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,IMemoryCache memoryCache,IDistributedCache distributedCache)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.memoryCache = memoryCache;
        this.distributedCache = distributedCache;
    }

    public async Task<ApiResponse<CountryResponse>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CountryRequest, Country>(request.Request);
        await unitOfWork.CountryRepository.Insert(mapped);
        await unitOfWork.Complete();
        
        memoryCache.Remove("countryList");
        await distributedCache.RemoveAsync("countryList");

        var response = mapper.Map<CountryResponse>(mapped);
        return new ApiResponse<CountryResponse>(response);
    }

    public async Task<ApiResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var mapped = mapper.Map<CountryRequest, Country>(request.Request);
        mapped.Id = request.CountryId;
        unitOfWork.CountryRepository.Update(mapped);
        await unitOfWork.Complete();
        
        memoryCache.Remove("countryList");
        await distributedCache.RemoveAsync("countryList");
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.CountryRepository.Delete(request.CountryId);
        await unitOfWork.Complete();
        
        memoryCache.Remove("countryList");
        await distributedCache.RemoveAsync("countryList");
        return new ApiResponse();
    }
}