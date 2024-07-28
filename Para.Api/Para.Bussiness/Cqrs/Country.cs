using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateCountryCommand(CountryRequest Request) : IRequest<ApiResponse<CountryResponse>>;
public record UpdateCountryCommand(long CountryId,CountryRequest Request) : IRequest<ApiResponse>;
public record DeleteCountryCommand(long CountryId) : IRequest<ApiResponse>;

public record GetAllCountryQuery() : IRequest<ApiResponse<List<CountryResponse>>>;
public record GetAllCountryFromCacheQuery() : IRequest<ApiResponse<List<CountryResponse>>>;
public record GetCountryByIdQuery(long CountryId) : IRequest<ApiResponse<CountryResponse>>;