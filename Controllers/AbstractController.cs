using Microsoft.AspNetCore.Mvc;
using teste_carteira_virtual.Application.Abstractions;

namespace teste_carteira_virtual.Controllers
{
    public class AbstractController : ControllerBase
    {
        public AbstractController(IModelStateAccessor accessor)
        {
            accessor.ModelState = ModelState;
        }
    }
}