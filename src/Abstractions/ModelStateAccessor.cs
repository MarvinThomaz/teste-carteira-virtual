using Microsoft.AspNetCore.Mvc.ModelBinding;
using teste_carteira_virtual.Application.Abstractions;

namespace teste_carteira_virtual.Abstractions
{
    public class ModelStateAccessor : IModelStateAccessor
    {
        public ModelStateDictionary ModelState { get; set; }
    }
}