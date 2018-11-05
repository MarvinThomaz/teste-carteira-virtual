using teste_carteira_virtual.Domain.Abstractions;

namespace teste_carteira_virtual.Abstractions
{
    public class PagingParametersAccessor : IPagingParametersAccessor
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }
}