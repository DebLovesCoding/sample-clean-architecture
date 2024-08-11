using FluentValidation;
using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Repository.Interfaces;
using Intelligent.Solution.Domain.Entities;

namespace Intelligent.Solution.Infrastructure.Validators
{
    public class ProductCategoryDeleteRequestValidator : AbstractValidator<ProductCategoryDeleteRequest>
    {
        private readonly IGenericRepository<ProductCategory> _repository;

        public ProductCategoryDeleteRequestValidator(IGenericRepository<ProductCategory> repository)
        {
            _repository = repository;
            RuleFor(x => x).Must((x) => { return IsIdValid(x.Id); }).WithMessage(string.Format(ValidationMessageConstants.InvalidIdMessage, "Category Id"));
        }

        private bool IsIdValid(int id)
        {
            return _repository.GetByIdAsync(id, CancellationToken.None).Result != null;
        }
    }
}
