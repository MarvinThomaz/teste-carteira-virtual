using teste_carteira_virtual.Domain.Enums;

namespace teste_carteira_virtual.Domain.Base
{
    public class ValidationResponse
    {
        public ResponseType Type { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
    }
}