using Orange_task.Domain.Entities;
using Orange_task.DTOs;

namespace Orange_task.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<PaginationResultDto<Customer>> GetCustomersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Customer>> GetCustomersWithExpiredContractsAsync();
        Task<IEnumerable<Customer>> GetCustomersWithContractsExpiringWithinOneMonthAsync();
        Task<Dictionary<string, int>> GetCustomerCountsByServiceTypeAsync();
        Task<Dictionary<int, Dictionary<int, int>>> GetCustomerCountPerMonthPerYearAsync();
    }
}
