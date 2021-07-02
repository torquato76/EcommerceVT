using EcommerceVT.Model;
using FluentValidation;

namespace EcommerceVT.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.Login).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
        }
    }
}