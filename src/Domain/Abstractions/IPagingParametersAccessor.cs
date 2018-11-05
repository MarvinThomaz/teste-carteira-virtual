namespace teste_carteira_virtual.Domain.Abstractions
{
    public interface IPagingParametersAccessor
    {
         int Page { get; set; }
         int RecordsPerPage { get; set; }
         int TotalPages { get; }
         int TotalItems { get; set; }
         int Skip { get; }
    }
}