using EcommerceVT.Model;
using FluentValidation;

namespace EcommerceVT.Validator
{
    public class MessageValidator : AbstractValidator<Messages>
    {
        public MessageValidator()
        {
            RuleFor(p => p.Message).NotEmpty();
            RuleFor(p => p.Title).NotEmpty();
        }
    }
}