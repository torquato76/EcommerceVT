using EcommerceVT.Model;
using FluentValidation;

namespace EcommerceVT.Validator
{
    public class InviteValidator : AbstractValidator<InvitePost>
    {
        public InviteValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Email).NotEmpty();
        }
    }
}