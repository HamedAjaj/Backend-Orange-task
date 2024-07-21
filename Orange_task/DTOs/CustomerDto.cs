namespace Orange_task.DTOs
{
    public class CustomerDto
    {
            public int id { get; set; }
            public string CustomerName { get; set; }
            public string Service { get; set; }
            public DateOnly ContractDate { get; set; }
            public DateOnly ContractExpiryDate { get; set; }

    }
}
