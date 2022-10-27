using HAHN.Domain.Responses;

namespace HAHN.Domain.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public ManagerResponse<object> Validate()
        {
            var ret = new ManagerResponse<object>();
            if (string.IsNullOrEmpty(Name))
            {
                ret.Add("Name is required.");
            }
            if (!string.IsNullOrEmpty(Phone) && Phone.Length > 35)
            {
                ret.Add("Phone max size is 35 characters.");
            }
            if (!string.IsNullOrEmpty(Email) && Email.Length > 75)
            {
                ret.Add("Email max size is 75 characters.");
            }
            return ret;
        }
    }
}
