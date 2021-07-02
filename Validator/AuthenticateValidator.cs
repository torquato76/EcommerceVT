using EcommerceVT.Model;
using FluentValidation;

namespace EcommerceVT.Validator
{
    public class AuthenticateValidator : AbstractValidator<Authenticate>
    {
        public AuthenticateValidator()
        {
            RuleFor(p => p.Login).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
        }
    }
}