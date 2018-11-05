using System.Collections.Generic;

namespace teste_carteira_virtual.Domain.Base
{
    public class CollectionResponse<T> : ObjectResponse<IEnumerable<T>>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int RecordsPerPage { get; set; }
    }
}