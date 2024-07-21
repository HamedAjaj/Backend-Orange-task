using Orange_task.DTOs;

namespace Orange_task.Domain.IService
{
    public interface ICustomerService
    {
        Task<PaginationResultDto<CustomerDto>> GetCustomersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CustomerDto>> GetCustomersWithExpiredContractsAsync();
        Task<IEnumerable<CustomerDto>> GetCustomersWithContractsExpiringWithinOneMonthAsync();
        Task<Dictionary<string, int>> GetCustomerCountsByServiceTypeAsync();
        Task<Dictionary<int, Dictionary<int, int>>> GetCustomerCountPerMonthPerYearAsync();
    }
}
