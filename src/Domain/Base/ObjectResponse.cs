using System.Collections.Generic;
using System.Linq;
using teste_carteira_virtual.Domain.Enums;

namespace teste_carteira_virtual.Domain.Base
{
    public class ObjectResponse<T>
    {
        public T Data { get; set; }
        public IEnumerable<ValidationResponse> Validations { get; set; }
        public bool Success { get; set; }
        public ResponseType? Type => Validations?.First().Type ?? null;
    }
}