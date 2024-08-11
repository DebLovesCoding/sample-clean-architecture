using FluentValidation;
using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Repository.Interfaces;
using Intelligent.Solution.Domain.Entities;

namespace Intelligent.Solution.Infrastructure.Validators
{
    public class ProductCategoryModifyRequestValidator : AbstractValidator<ProductCategoryModifyRequest>
    {
        private readonly IGenericRepository<ProductCategory> _repository;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public ProductCategoryModifyRequestValidator(IGenericRepository<ProductCategory> repository)
        {
            _repository = repository;
            RuleFor(x => x.ProductCategory.Name).NotEmpty().WithMessage(string.Format(ValidationMessageConstants.MandatoryInputMessage, "Category Name"));
            RuleFor(x => x.ProductCategory).Must((x) => { return IsIdValid(x.Id); }).WithMessage(string.Format(ValidationMessageConstants.InvalidIdMessage, "Category Id"));
        }

        private bool IsIdValid(int id)
        {
            return _repository.GetByIdAsync(id, CancellationToken.None).Result != null;
        }

#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
