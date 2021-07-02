using EcommerceVT.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceVT.Validator
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(p => p.Zip_Code).NotNull().NotEqual(0);
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.City).NotEmpty();
            RuleFor(p => p.State).NotEmpty();
            RuleFor(p => p.Lat).NotNull().NotEqual(0);
            RuleFor(p => p.Lng).NotNull().NotEqual(0);
        }
    }
}