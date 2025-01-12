using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CodeAssessment.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty().GreaterThan(0).WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductDescrption).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
