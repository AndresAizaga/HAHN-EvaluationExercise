using HAHN.Domain.Models;
using HAHN.Domain.Responses;
using HAHN.Infrastructure.DataLayer;

namespace HAHN.Application.Contacts
{
    public interface  IContactService
    {
        Task<ManagerResponse<ContactModel>> GetItem(System.Int32 id);
        Task<ManagerResponse<List<ContactModel>>> GetList(System.String name = null, System.String phone = null, System.String email = null);
        Task<List<string>> UniqueCheck(ContactModel model, HahnDataContext db);
        Task<ManagerResponse<object>> PutItem(ContactModel model);
        Task<ManagerResponse<System.Int32?>> PostItem(ContactModel model);
        Task<ManagerResponse<object>> DeleteItem(System.Int32 id);
    }
}
