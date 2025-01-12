using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CodeAssessment.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.ProductDescrption).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
