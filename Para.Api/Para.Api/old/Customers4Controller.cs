using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [NonController]
    public class Customers4Controller : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public Customers4Controller(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            List<Customer> entityList = await unitOfWork.CustomerRepository.GetAll();
            var mappedList = mapper.Map<List<Customer>, List<CustomerResponse>>(entityList);
            return new ApiResponse<List<CustomerResponse>>(mappedList);
        }

        [HttpGet("{customerId}")]
        public async Task<ApiResponse<CustomerResponse>> Get(long customerId)
        {
            Customer entity = await unitOfWork.CustomerRepository.GetById(customerId);
            var mapped = mapper.Map<Customer, CustomerResponse>(entity);
            return new ApiResponse<CustomerResponse>(mapped);
        }

        [HttpPost]
        public async Task<ApiResponse> Post([FromBody] CustomerRequest value)
        {
            var mapped = mapper.Map<CustomerRequest, Customer>(value);
            await unitOfWork.CustomerRepository.Insert(mapped);
            await unitOfWork.CustomerRepository.Insert(mapped);
            await unitOfWork.CompleteWithTransaction();
            return new ApiResponse();
        }

        [HttpPut("{customerId}")]
        public async Task<ApiResponse> Put(long customerId, [FromBody] CustomerRequest value)
        {
            var mapped = mapper.Map<CustomerRequest, Customer>(value);
            unitOfWork.CustomerRepository.Update(mapped);
            await unitOfWork.Complete();
            return new ApiResponse();
        }

        [HttpDelete("{customerId}")]
        public async Task<ApiResponse> Delete(long customerId)
        {
            await unitOfWork.CustomerRepository.Delete(customerId);
            await unitOfWork.Complete();
            return new ApiResponse();
        }
    }
}