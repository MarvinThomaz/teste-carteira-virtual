using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace teste_carteira_virtual.Application.Abstractions
{
    public interface IModelStateAccessor
    {
         ModelStateDictionary ModelState { get; set; }
    }
}