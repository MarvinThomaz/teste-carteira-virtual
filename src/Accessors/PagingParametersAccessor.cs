using teste_carteira_virtual.Domain.Abstractions;

namespace teste_carteira_virtual.Accessors
{
    public class PagingParametersAccessor : IPagingParametersAccessor
    {
        private int _recordsPerPage;

        public int Page { get; set; }
        public int RecordsPerPage { get => _recordsPerPage == 0 ? 10 : _recordsPerPage; set => _recordsPerPage = value; }
        public int TotalPages => TotalItems < RecordsPerPage ? TotalItems : TotalItems / RecordsPerPage;
        public int TotalItems { get; set; }
        public int Skip => Page * RecordsPerPage;
    }
}