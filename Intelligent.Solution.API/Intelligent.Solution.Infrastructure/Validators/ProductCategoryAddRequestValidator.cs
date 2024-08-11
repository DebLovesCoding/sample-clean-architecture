using FluentValidation;
using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Commands;

namespace Intelligent.Solution.Infrastructure.Validators
{
    public class ProductCategoryAddRequestValidator : AbstractValidator<ProductCategoryAddRequest>
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public ProductCategoryAddRequestValidator()
        {
            RuleFor(x => x.ProductCategory.Name).NotEmpty().WithMessage(string.Format(ValidationMessageConstants.MandatoryInputMessage, "Category Name"));
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
