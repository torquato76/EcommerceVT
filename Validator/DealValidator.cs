using EcommerceVT.Model;
using FluentValidation;

namespace EcommerceVT.Validator
{
    public class DealValidator : AbstractValidator<Deal>
    {
        public DealValidator()
        {
            RuleFor(p => p.Description).NotEmpty();
        }
    }
}