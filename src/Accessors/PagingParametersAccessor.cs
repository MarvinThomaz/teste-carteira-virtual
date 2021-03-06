using teste_carteira_virtual.Domain.Abstractions;

namespace teste_carteira_virtual.Accessors
{
    public class PagingParametersAccessor : IPagingParametersAccessor
    {
        private int _recordsPerPage;
        private int _page;

        public int Page { get => _page <= 0 ? _page = 1 : _page; set => _page = value; }
        public int Index { get => Page > 0 ? (Page - 1) : Page;  set => Page = value; }
        public int RecordsPerPage { get => _recordsPerPage <= 0 ? 10 : _recordsPerPage; set => _recordsPerPage = value; }
        public int TotalPages => TotalItems < RecordsPerPage ? TotalItems : TotalItems / RecordsPerPage;
        public int TotalItems { get; set; }
        public int Skip => Index * RecordsPerPage;
    }
}