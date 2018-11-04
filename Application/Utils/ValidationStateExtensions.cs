using System.Collections.Generic;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Enums;

namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    public static class ValidationStateExtensions
    {
        public static IEnumerable<ValidationResponse> ValidateModel(this ModelStateDictionary modelState)
        {
            if(modelState.IsValid)
            {
                yield break;
            }

            foreach(var property in modelState.Keys)
            {
                ModelStateEntry entry;

                modelState.TryGetValue(property, out entry);

                foreach(var error in entry?.Errors)
                {
                    yield return new ValidationResponse
                    {
                        Type = ResponseType.ValidationErrorOfObject,
                        Message = error.ErrorMessage,
                        Property = property
                    }
                }
            }
        }
    }
}