using System.Threading.Tasks;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Models;

namespace teste_carteira_virtual.Domain.Services
{
    public interface IClientApplicationService
    {
         Task<CollectionResponse<GetClientViewModel>> GetClientFromPartOfName(string name);
         Task<ObjectResponse<GetClientViewModel>> GetClientFromDocumentId(string documentId);
    }
}