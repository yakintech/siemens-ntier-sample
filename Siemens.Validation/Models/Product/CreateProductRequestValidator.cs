using FluentValidation;
using Siemens.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.Validation.Models.Product
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequestDto>
    {

        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name alanı boş bırakılamaz")
                .MaximumLength(10)
                .WithMessage("Name alanı maksimum 10 karakter olabilir");


            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description alanı boş bırakılamaz");
        }
    }
}
