using FluentValidation;
using HAHN.Domain.Models;

namespace HAHN.Validations
{
    public class ContactValidation : AbstractValidator<ContactModel>
    {
        public ContactValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Age).NotEmpty();
        }
    }
}
