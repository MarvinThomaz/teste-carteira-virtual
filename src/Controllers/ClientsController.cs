using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using teste_carteira_virtual.Paging;
using teste_carteira_virtual.Accessors;
using teste_carteira_virtual.Application.Abstractions;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Services;

namespace teste_carteira_virtual.Controllers
{
    [Route("api/clients")]
    public class ClientsController : AbstractController
    {
        private readonly IClientApplicationService _service;

        public ClientsController(IClientApplicationService service, IModelStateAccessor accessor) : base(accessor)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientFromPartOfName([FromQuery] string name, [FromQuery] PagingParameters parameters)
        {
            var response = await _service.GetClientFromPartOfName(name);

            return Ok(response);
        }


        [HttpGet]
        [Route("{documentId}")]
        public async Task<IActionResult> GetClientFromDocumentId(string documentId)
        {
            var response = await _service.GetClientFromDocumentId(documentId);

            if(response.Type == ResponseType.NotFoundedObject)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}