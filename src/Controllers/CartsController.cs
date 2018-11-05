using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using teste_carteira_virtual.Accessors;
using teste_carteira_virtual.Application.Abstractions;
using teste_carteira_virtual.Domain.Abstractions;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Services;
using teste_carteira_virtual.Paging;

namespace teste_carteira_virtual.Controllers
{
    [Route("api/carts")]
    public class CartsController : AbstractController
    {
        private readonly ICartApplicationService _service;

        public CartsController(ICartApplicationService service, IModelStateAccessor accessor) : base(accessor)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCartModel request)
        {
            var response = await _service.Create(request);

            if(response.Type == ResponseType.ValidationErrorOfObject)
            {
                return UnprocessableEntity(response);
            }
            else
            {
                return Created($"api/carts/{response.Data.ExternalKey}", response);
            }
        }

        [HttpPut]
        [Route("{externalKey}")]
        public async Task<IActionResult> UpdateCartValue(string externalKey, [FromBody] UpdateCartModel request)
        {
            var response = await _service.UpdateCart(externalKey, request);

            if(response.Type == ResponseType.ValidationErrorOfObject)
            {
                return UnprocessableEntity(response);
            }
            else if(response.Type == ResponseType.NotFoundedObject)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("clients/{documentId}")]
        public async Task<IActionResult> GetActiveCartFromClient(string documentId)
        {
            var response = await _service.GetActiveCartFromClient(documentId);

            if(response.Type == ResponseType.NotFoundedObject)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("{externalKey}")]
        public async Task<IActionResult> GetCartFromExternalKey(string externalKey)
        {
            var response = await _service.GetCartFromExternalKey(externalKey);

            if(response.Type == ResponseType.NotFoundedObject)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveCarts([FromQuery] PagingParameters parameters)
        {
            var response = await _service.GetActiveCarts();

            return Ok(response);
        }
    }
}
