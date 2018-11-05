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
        private readonly ITransactionManager _transactionManager;
        private readonly IPagingParametersAccessor _pagingParametersAccessor;
        private readonly IAddCartCommand _addCartCommand;
        private readonly IGetActiveCartFromClientQuery _getActiveCartFromClientQuery;
        private readonly IGetActiveCartsQuery _getActiveCartsQuery;
        private readonly IGetCartFromExternalKeyQuery _getCartFromExternalKeyQuery;
        private readonly IUpdateCartValueCommand _updateCartValueCommand;
        private readonly IDisableCartCommand _disableCartCommand;

        public CartApplicationService
        (
            ITransactionManager transactionManager,
            IPagingParametersAccessor pagingParametersAccessor,
            IAddCartCommand addCartCommand,
            IGetActiveCartFromClientQuery getActiveCartFromClientQuery,
            IGetActiveCartsQuery getActiveCartsQuery,
            IGetCartFromExternalKeyQuery getCartFromExternalKeyQuery,
            IUpdateCartValueCommand updateCartValueCommand,
            IDisableCartCommand disableCartCommand
        )
        {
            _transactionManager = transactionManager;
            _pagingParametersAccessor = pagingParametersAccessor;
            _addCartCommand = addCartCommand;
            _getActiveCartFromClientQuery = getActiveCartFromClientQuery;
            _getActiveCartsQuery = getActiveCartsQuery;
            _getCartFromExternalKeyQuery = getCartFromExternalKeyQuery;
            _updateCartValueCommand = updateCartValueCommand;
            _disableCartCommand = disableCartCommand;
        }

        public async Task<ObjectResponse<GetCartViewModel>> Create(AddCartModel model)
        {
            _addCartCommand.Model = model;

            using(_transactionManager.Begin())
            {
                var result = await _addCartCommand.Execute();

                _transactionManager.Commit();
            }

            if(_addCartCommand.Validations?.Any() == true)
            {
                return new ObjectResponse<GetCartViewModel>
                {
                    Validations = _addCartCommand.Validations,
                    Success = false
                };
            }

            return new ObjectResponse<GetCartViewModel>
            {
                Data = result,
                Success = true
            };
        }

        public async Task<ObjectResponse<GetCartViewModel>> GetActiveCartFromClient(string documentId)
        {
            var result = await _getActiveCartFromClientQuery.Execute(documentId);

            if(_getActiveCartFromClientQuery.Validations?.Any() == true)
            {
                return new ObjectResponse<GetCartViewModel>
                {
                    Validations = _getActiveCartFromClientQuery.Validations,
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

            if(_getCartFromExternalKeyQuery.Validations?.Any() == true)
            {
                return new ObjectResponse<GetCartViewModel>
                {
                    Validations = _getCartFromExternalKeyQuery.Validations,
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

            using(_transactionManager.Begin())
            {
                if(model.ChargeValue != null)
                {
                    _updateCartValueCommand.Model = model;

                    result = await _updateCartValueCommand.Execute(externalKey);
                }

                if(model.IsActive == false)
                {
                    result = await _disableCartCommand.Execute(externalKey);
                }

                _transactionManager.Commit();
            }
            
            return new ObjectResponse<GetCartViewModel>
            {
                Data = result,
                Success = true
            };
        }
    }
}