using AutoMapper;
using Orange_task.Domain.Interfaces;
using Orange_task.Domain.IService;
using Orange_task.DTOs;

namespace Orange_task.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResultDto<CustomerDto>> GetCustomersAsync(int pageNumber, int pageSize)
        {
            var customers = await _customerRepository.GetCustomersAsync(pageNumber, pageSize);
            return new PaginationResultDto<CustomerDto>
            {
                TotalCount = customers.TotalCount,
                Items = _mapper.Map<IEnumerable<CustomerDto>>(customers.Items)
            };
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersWithExpiredContractsAsync()
        {
            var customers = await _customerRepository.GetCustomersWithExpiredContractsAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersWithContractsExpiringWithinOneMonthAsync()
        {
            var customers = await _customerRepository.GetCustomersWithContractsExpiringWithinOneMonthAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<Dictionary<string, int>> GetCustomerCountsByServiceTypeAsync()
        {
            return await _customerRepository.GetCustomerCountsByServiceTypeAsync();
        }

        public async Task<Dictionary<int, Dictionary<int, int>>> GetCustomerCountPerMonthPerYearAsync()
        {
            return await _customerRepository.GetCustomerCountPerMonthPerYearAsync();
        }
    }
}
