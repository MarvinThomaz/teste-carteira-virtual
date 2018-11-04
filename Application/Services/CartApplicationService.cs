using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using teste_carteira_virtual.Application.Abstractions;
using teste_carteira_virtual.Domain.Abstractions;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Commands;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Queries;
using teste_carteira_virtual.Domain.Services;

namespace teste_carteira_virtual.Application.Services
{
    public class CartApplicationService : ICartApplicationService
    {
        private readonly IPagingParametersAccessor _pagingParametersAccessor;
        private readonly IModelStateAccessor _modelStateAccessor;
        private readonly IAddCartCommand _addCartCommand;
        private readonly IGetActiveCartFromClientQuery _getActiveCartFromClientQuery;
        private readonly IGetActiveCartsQuery _getActiveCartsQuery;
        private readonly IGetCartFromExternalKeyQuery _getCartFromExternalKeyQuery;
        private readonly IUpdateCartValueCommand _updateCartValueCommand;
        private readonly IDisableCartCommand _disableCartCommand;

        public CartApplicationService
        (
            IPagingParametersAccessor pagingParametersAccessor,
            IModelStateAccessor modelStateAccessor,
            IAddCartCommand addCartCommand,
            IGetActiveCartFromClientQuery getActiveCartFromClientQuery,
            IGetActiveCartsQuery getActiveCartsQuery,
            IGetCartFromExternalKeyQuery getCartFromExternalKeyQuery,
            IUpdateCartValueCommand updateCartValueCommand,
            IDisableCartCommand disableCartCommand
        )
        {
            _pagingParametersAccessor = pagingParametersAccessor;
            _modelStateAccessor = modelStateAccessor;
            _addCartCommand = addCartCommand;
            _getActiveCartFromClientQuery = getActiveCartFromClientQuery;
            _getActiveCartsQuery = getActiveCartsQuery;
            _getCartFromExternalKeyQuery = getCartFromExternalKeyQuery;
            _updateCartValueCommand = updateCartValueCommand;
            _disableCartCommand = disableCartCommand;
        }

        public async Task<ObjectResponse<GetCartViewModel>> Create(AddCartModel model)
        {
            var validations = _modelStateAccessor.ModelState.ValidateModel();

            if(validations.Any())
            {
                return new ObjectResponse<GetCartViewModel>
                {
                    Validations = validations,
                    Success = false
                };
            }

            _addCartCommand.Model = model;

            var result = await _addCartCommand.Execute();

            return new ObjectResponse<GetCartViewModel>
            {
                Data = result,
                Success = true
            };
        }

        public async Task<ObjectResponse<GetCartViewModel>> GetActiveCartFromClient(string documentId)
        {
            var result = await _getActiveCartFromClientQuery.Execute(documentId);

            if(result == null)
            {
                return new ObjectResponse<GetCartViewModel>
                {
                    Validations = new List<ValidationResponse>
                    {
                        new ValidationResponse
                        {
                            Type = ResponseType.NotFoundedObject,
                            Property = nameof(documentId)
                        }
                    },
                    Success = false
                };
            }

            return new ObjectResponse<GetCartViewModel>
            {
                Data = result,
                Success = true
            };
        }

        public async Task<CollectionResponse<GetCartViewModel>> GetActiveCarts()
        {
            var result = await _getActiveCartsQuery.Execute();

            return new CollectionResponse<GetCartViewModel>
            {
                Page = _pagingParametersAccessor.Page,
                RecordsPerPage = _pagingParametersAccessor.RecordsPerPage,
                TotalItems = _pagingParametersAccessor.TotalItems,
                TotalPages = _pagingParametersAccessor.TotalPages,
                Data = result,
                Success = true
            };
        }

        public async Task<ObjectResponse<GetCartViewModel>> GetCartFromExternalKey(string externalKey)
        {
            var result = await _getCartFromExternalKeyQuery.Execute(externalKey);

            if(result == null)
            {
                return new ObjectResponse<GetCartViewModel>
                {
                    Validations = new List<ValidationResponse>
                    {
                        new ValidationResponse
                        {
                            Type = ResponseType.NotFoundedObject,
                            Property = nameof(externalKey)
                        }
                    },
                    Success = false
                };
            }

            return new ObjectResponse<GetCartViewModel>
            {
                Data = result,
                Success = true
            };
        }

        public async Task<ObjectResponse<GetCartViewModel>> UpdateCart(string externalKey, UpdateCartModel model)
        {
            if(model.ChargeValue == null && model.IsActive != false)
            {
                return new ObjectResponse<GetCartViewModel>
                {
                    Validations = new List<ValidationResponse>
                    {
                        new ValidationResponse
                        {
                            Type = ResponseType.NotFoundedObject,
                            Property = nameof(model)
                        }
                    },
                    Success = false
                };
            }

            GetCartViewModel result = null;

            if(model.ChargeValue != null)
            {
                _updateCartValueCommand.Model = model;

                result = await _updateCartValueCommand.Execute(externalKey);
            }

            if(model.IsActive == false)
            {
                result = await _disableCartCommand.Execute(externalKey);
            }

            return new ObjectResponse<GetCartViewModel>
            {
                Data = result,
                Success = true
            };
        }
    }
}