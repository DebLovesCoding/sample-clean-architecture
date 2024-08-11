using FluentValidation;
using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Repository.Interfaces;
using Intelligent.Solution.Domain.Entities;
using Intelligent.Solution.Common;

namespace Intelligent.Solution.Infrastructure.Validators
{
    public class ProductDeleteRequestValidator : AbstractValidator<ProductDeleteRequest>
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductDeleteRequestValidator(IGenericRepository<Product> repository)
        {
            _repository = repository;
            RuleFor(x => x).Must((x) => { return IsIdValid(x.Id); }).WithMessage(string.Format(ValidationMessageConstants.InvalidIdMessage, "Product"));
        }

        private bool IsIdValid(int id)
        {
            return _repository.GetByIdAsync(id, CancellationToken.None).Result != null;
        }
    }
}
