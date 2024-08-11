using FluentValidation;
using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Repository.Interfaces;
using Intelligent.Solution.Domain.Entities;

namespace Intelligent.Solution.Infrastructure.Validators
{
    public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
    {
        private readonly IGenericRepository<ProductCategory> _repository;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public ProductAddRequestValidator(IGenericRepository<ProductCategory> repository) 
        {
            _repository = repository;
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage(string.Format(ValidationMessageConstants.MandatoryInputMessage, "Product Name"));
            RuleFor(x => x.Product.Code).NotEmpty().WithMessage(string.Format(ValidationMessageConstants.MandatoryInputMessage, "Product Code"));
            RuleFor(x => x.Product).Must((x) => { return IsCategoryIdValid(x.ProductCategoryId); }).WithMessage(string.Format(ValidationMessageConstants.InvalidIdMessage, "Category Id"));
        }

#pragma warning disable CS8629 // Nullable value type may be null.
        private bool IsCategoryIdValid(int? categoryId)
        {
            return _repository.GetByIdAsync(categoryId.Value, CancellationToken.None).Result != null;
        }
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
