using ExcelDataReader;
using Orange_task.Domain.Entities;
using System.Text.Json;

namespace Orange_task.Repository.Data
{
    public static class CustomerDataSeeding
    {
        public static async Task SeedAsync(CustomerDbContext context)
        {
            if (!context.Customers.Any())
            {
                var customers = new List<Customer>();
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var stream = System.IO.File.Open("../Orange_task/Repository/Data/DataSeed/MOCK_DATA.xls", FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth == 0) continue; // Skip header row

                            var customer = new Customer
                            {
                               // id = Convert.ToInt32(reader.GetValue(0)),
                                CustomerName = reader.GetValue(1).ToString(),
                                Service = reader.GetValue(2).ToString(),
                                ContractDate = DateOnly.FromDateTime(Convert.ToDateTime(reader.GetValue(3))),
                                ContractExpiryDate = DateOnly.FromDateTime(Convert.ToDateTime(reader.GetValue(4)))
                            };
                            customers.Add(customer);
                        }
                    }
                }

                if (customers.Count > 0)
                {
                    await context.Set<Customer>().AddRangeAsync(customers);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
