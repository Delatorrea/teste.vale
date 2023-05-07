namespace Domain.PurchaseContext.DTOs
{
    public class ResponseGetAllDTO<T>
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public T Data { get; set; }
    }
}
