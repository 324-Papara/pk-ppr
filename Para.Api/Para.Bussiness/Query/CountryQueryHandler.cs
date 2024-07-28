using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Para.Base;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Query;

public class CountryQueryHandler : 
    IRequestHandler<GetAllCountryQuery,ApiResponse<List<CountryResponse>>>,
    IRequestHandler<GetCountryByIdQuery,ApiResponse<CountryResponse>>
    
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IMemoryCache memoryCache;
    private readonly ISessionContext sessionContext;

    public CountryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,IMemoryCache memoryCache,ISessionContext sessionContext)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.memoryCache = memoryCache;
        this.sessionContext = sessionContext;
    }
    
    public async Task<ApiResponse<List<CountryResponse>>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
    {
        var userId = sessionContext.Session.UserId;
        var checkResult = memoryCache.TryGetValue("countryList", out ApiResponse<List<CountryResponse>> cacheData);
        if (checkResult)
            return cacheData;
        
        List<Country> entityList = await unitOfWork.CountryRepository.GetAll();
        var mappedList = mapper.Map<List<CountryResponse>>(entityList);
        var response =  new ApiResponse<List<CountryResponse>>(mappedList);


        if (entityList.Any())
        {
            var cacheOptions = new MemoryCacheEntryOptions()
            {
                Priority = CacheItemPriority.High,
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromHours(1)
            };
            memoryCache.Set("countryList", response, cacheOptions);
        }
        return response;
    }

    public async Task<ApiResponse<CountryResponse>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CountryRepository.GetById(request.CountryId);
        var mapped = mapper.Map<CountryResponse>(entity);
        return new ApiResponse<CountryResponse>(mapped);
    }
}