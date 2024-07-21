using Microsoft.EntityFrameworkCore;
using Orange_task.Domain.Entities;
using Orange_task.Domain.Interfaces;
using Orange_task.DTOs;
using Orange_task.Repository.Data;
using System;

namespace Orange_task.Repository.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationResultDto<Customer>> GetCustomersAsync(int pageNumber, int pageSize)
        {
            var customers = await _context.Customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalCount = await _context.Customers.CountAsync();

            return new PaginationResultDto<Customer>
            {
                TotalCount = totalCount,
                Items = customers
            };
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithExpiredContractsAsync()
        {
            return await _context.Customers
                .Where(c => c.ContractExpiryDate < DateOnly.FromDateTime(DateTime.Now))
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithContractsExpiringWithinOneMonthAsync()
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            var nextMonth = now.AddMonths(1);

            return await _context.Customers
                .Where(c => c.ContractExpiryDate >= now && c.ContractExpiryDate <= nextMonth)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetCustomerCountsByServiceTypeAsync()
        {
            return await _context.Customers
                .GroupBy(c => c.Service)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }

        public async Task<Dictionary<int, Dictionary<int, int>>> GetCustomerCountPerMonthPerYearAsync()
        {
            var monthlyCounts = await GetMonthlyCustomerCountsAsync();
            return GroupMonthlyCountsByYear(monthlyCounts);
        }


        private async Task<List<CustomerMonthlyCountDto>> GetMonthlyCustomerCountsAsync()
        {
            return await _context.Customers
                .GroupBy(c => new { c.ContractDate.Year, c.ContractDate.Month })
                .Select(g => new CustomerMonthlyCountDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();
        }

        private Dictionary<int, Dictionary<int, int>> GroupMonthlyCountsByYear(List<CustomerMonthlyCountDto> monthlyCounts)
        {
            return monthlyCounts
                .GroupBy(r => r.Year)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary( gm => gm.Month,  gm => gm.Count )
                );
        }

    }
}
