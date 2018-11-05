namespace teste_carteira_virtual.Domain.Abstractions
{
    public interface IPagingParametersAccessor
    {
         int Page { get; set; }
         int RecordsPerPage { get; set; }
         int TotalPages { get; set; }
         int TotalItems { get; set; }
    }
}