using HAHN.Domain.Models;
using HAHN.Domain.Responses;
using HAHN.Infrastructure.DataLayer;
using HAHN.Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace HAHN.Application.Contacts
{
    public class ContactService : IContactService
    {
        public  async Task<ManagerResponse<ContactModel>> GetItem(System.Int32 id)
        {
            var ret = new ManagerResponse<ContactModel>();
            var retObj = new ContactModel();
            ret.SetData(retObj);
            using (var db = new HahnDataContext())
            {
                var resp = await db.Contact.FirstOrDefaultAsync(x => x.Id == id);
                if (resp == null)
                {
                    return ret.Add("No resource found with that id", ResponseErrorCodes.ResourceNotFound);
                }
                retObj.Id = resp.Id;
                retObj.Name = resp.Name;
                retObj.Email = resp.Email;
                retObj.Phone = resp.Phone;
                retObj.Age = resp.Age;
            }
            return ret;
        }

        public async Task<ManagerResponse<List<ContactModel>>> GetList(System.String name = null, System.String phone = null, System.String email = null)
        {
            var ret = new ManagerResponse<List<ContactModel>>();
            using (var db = new HahnDataContext())
            {

                var blogs = db.Contact
                .Where(b => !string.IsNullOrEmpty(name) ? b.Name.ToLower() == name.ToLower() : true)
                .Where(b => !string.IsNullOrEmpty(phone) ? b.Phone.ToLower() == phone.ToLower() : true)
                .Where(b => !string.IsNullOrEmpty(email) ? b.Email.ToLower() == email.ToLower() : true)
                .OrderBy(b => b.Id)
                ;

                ret = await Utils.PageList<ContactModel>(ret, blogs, 1, 50);
            }
            return ret;
        }

        public async Task<List<string>> UniqueCheck(ContactModel model, HahnDataContext db)
        {
            var ret = new List<string>();
            {
                var resp = await (from contacts in db.Contact
                                  where model.Id > 0 ? contacts.Id != model.Id : true
                                  where contacts.Name == model.Name
                                  where contacts.Phone == model.Phone
                                  where contacts.Email == model.Email
                                  where contacts.Age == model.Age
                                  select contacts
                    ).AnyAsync();
                if (resp)
                {
                    ret.Add("Contact");
                }
            }
            return ret;
        }

        public async Task<ManagerResponse<object>> PutItem(ContactModel model)
        {
            var ret = new ManagerResponse<object>();
            var resp = model.Validate();
            if (!resp.Success)
            {
                return ret.Add(resp);
            }
            using (var db = new HahnDataContext())
            {
                var u = await UniqueCheck(model, db);
                if (u.Count > 0)
                {
                    foreach (var r in u)
                    {
                        ret.Add(r + " already exist and is not unique");
                    }
                    return ret;
                }
                var entity = await db.Contact.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.Phone = model.Phone;
                    entity.Email = model.Email;
                    entity.Age = model.Age;
                    await Utils.SaveChangesAsync(db);
                }
                
            }
            if (ret.Success) { ret.SuccessMessages.Add("Your changes were successfully saved."); }
            return ret;
        }

        public async Task<ManagerResponse<System.Int32?>> PostItem(ContactModel model)
        {
            var ret = new ManagerResponse<System.Int32?>();
            var resp = model.Validate();
            if (!resp.Success)
            {
                return ret.Add(resp);
            }
            using (var db = new HahnDataContext())
            {
                var u = await UniqueCheck(model, db);
                if (u.Count > 0)
                {
                    foreach (var r in u)
                    {
                        ret.Add(r + " already exist and is not unique");
                    }
                    return ret;
                }
                var entity = new ContactModel();
                entity.Name = model.Name;
                entity.Phone = model.Phone;
                entity.Email = model.Email;
                entity.Age = model.Age;
                db.Contact.Add(entity);
                await Utils.SaveChangesAsync(db);
                ret.SetData(entity.Id);
            }
            if (ret.Success) { ret.SuccessMessages.Add("Your changes were successfully saved."); }
            return ret;
        }

        public async Task<ManagerResponse<object>> DeleteItem(System.Int32 id)
        {
            var ret = new ManagerResponse<object>();
            using (var db = new HahnDataContext())
            {
                var entity = db.Contact.Where(x => x.Id == id);
                db.Contact.RemoveRange(entity);
                await Utils.SaveChangesAsync(db);
            }
            return ret;
        }
    }
}
