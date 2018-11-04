using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Models;

namespace teste_carteira_virtual.Domain.Services
{
    public interface ICartApplicationService
    {
         Task<ObjectResponse<GetCartViewModel>> Create(AddCartModel model);
         Task<ObjectResponse<GetCartViewModel>> UpdateCart(string externalKey, UpdateCartValueModel model);
         Task<ObjectResponse<GetCartViewModel>> GetActiveCartFromClient(string documentId);
         Task<ObjectResponse<GetCartViewModel>> GetCartFromExternalKey(string externalKey);
         Task<CollectionResponse<GetCartViewModel>> GetActiveCarts();
    }
}