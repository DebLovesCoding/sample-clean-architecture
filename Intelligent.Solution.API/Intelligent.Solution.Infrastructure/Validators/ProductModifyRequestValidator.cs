using FluentValidation;
using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Repository.Interfaces;
using Intelligent.Solution.Domain.Entities;
using Intelligent.Solution.Common;

namespace Intelligent.Solution.Infrastructure.Validators
{
    public class ProductModifyRequestValidator : AbstractValidator<ProductModifyRequest>
    {
        private readonly IGenericRepository<ProductCategory> _categoryRepository;
        private readonly IGenericRepository<Product> _repository;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public ProductModifyRequestValidator(IGenericRepository<ProductCategory> categoryRepository, IGenericRepository<Product> repository)
        {
            _categoryRepository = categoryRepository;
            _repository = repository;
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage(string.Format(ValidationMessageConstants.MandatoryInputMessage, "Product Name"));
            RuleFor(x => x.Product.Code).NotEmpty().WithMessage(string.Format(ValidationMessageConstants.MandatoryInputMessage, "Product Code"));
            RuleFor(x => x.Product).Must((x) => { return IsCategoryIdValid(x.ProductCategoryId); }).WithMessage(string.Format(ValidationMessageConstants.InvalidIdMessage, "Category Id"));
            RuleFor(x => x.Product).Must((x) => { return IsIdValid(x.Id); }).WithMessage(string.Format(ValidationMessageConstants.InvalidIdMessage, "Product Id"));
        }

#pragma warning disable CS8629 // Nullable value type may be null.
        private bool IsCategoryIdValid(int? categoryId)
        {
            return _categoryRepository.GetByIdAsync(categoryId.Value, CancellationToken.None).Result != null;
        }

        private bool IsIdValid(int id)
        {
            return _repository.GetByIdAsync(id, CancellationToken.None).Result != null;
        }
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    }
}
