namespace Orange_task.DTOs
{
    public class PaginationResultDto<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
