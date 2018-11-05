using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Abstractions;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Domain.Models;
using teste_carteira_virtual.Domain.Queries;
using teste_carteira_virtual.Domain.Services;

namespace teste_carteira_virtual.Application.Services
{
    public class ClientApplicationService : IClientApplicationService
    {
        private readonly IPagingParametersAccessor _pagingParametersAccessor;
        private readonly IGetClientFromDocumentIdQuery _getClientFromDocumentIdQuery;
        private readonly IGetClientFromPartOfNameQuery _getClientFromPartOfNameQuery;

        public ClientApplicationService
        (
            IPagingParametersAccessor pagingParametersAccessor, 
            IGetClientFromDocumentIdQuery getClientFromDocumentIdQuery, 
            IGetClientFromPartOfNameQuery getClientFromPartOfNameQuery
        )
        {
            _pagingParametersAccessor = pagingParametersAccessor;
            _getClientFromDocumentIdQuery = getClientFromDocumentIdQuery;
            _getClientFromPartOfNameQuery = getClientFromPartOfNameQuery;
        }

        public async Task<ObjectResponse<GetClientViewModel>> GetClientFromDocumentId(string documentId)
        {
            var result = await _getClientFromDocumentIdQuery.Execute(documentId);

            if(_getClientFromDocumentIdQuery.Validations?.Any() == true)
            {
                return new ObjectResponse<GetClientViewModel>
                {
                    Validations = _getClientFromDocumentIdQuery.Validations,
                    Success = false
                };
            }

            return new ObjectResponse<GetClientViewModel>
            {
                Data = result,
                Success = true
            };
        }

        public async Task<CollectionResponse<GetClientViewModel>> GetClientFromPartOfName(string name)
        {
            var result = await _getClientFromPartOfNameQuery.Execute(name);

            return new CollectionResponse<GetClientViewModel>
            {
                Page = _pagingParametersAccessor.Page,
                TotalItems = _pagingParametersAccessor.TotalItems,
                TotalPages = _pagingParametersAccessor.TotalPages,
                RecordsPerPage = _pagingParametersAccessor.RecordsPerPage,
                Data = result,
                Success = true
            };
        }
    }
}