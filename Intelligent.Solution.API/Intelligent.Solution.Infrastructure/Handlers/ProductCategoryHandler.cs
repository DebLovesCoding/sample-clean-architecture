using AutoMapper;
using FluentValidation;
using Intelligent.Solution.Common;
using Intelligent.Solution.Domain.Entities;
using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Infrastructure.Dtos;
using Intelligent.Solution.Infrastructure.Queries;
using Intelligent.Solution.Infrastructure.Responses;
using Intelligent.Solution.Repository.Interfaces;
using MediatR;
using System.Text;

namespace Intelligent.Solution.Infrastructure.Handlers
{
    public class ProductCategoryHandler : IRequestHandler<ProductCategoryAddRequest, BaseResponse<ProductCategoryDto>>,
                        IRequestHandler<ProductCategoryModifyRequest, BaseResponse<ProductCategoryDto>>,
                        IRequestHandler<ProductCategoryDeleteRequest, EntityDeleteResponse>,
                        IRequestHandler<ProductCategoryDetailRequest, BaseResponse<ProductCategoryDto>>
    {
        private readonly IGenericRepository<ProductCategory> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductCategoryAddRequest> _addValidator;
        private readonly IValidator<ProductCategoryModifyRequest> _modifyValidator;
        private readonly IValidator<ProductCategoryDeleteRequest> _deleteValidator;

        public ProductCategoryHandler(IGenericRepository<ProductCategory> repository,
            IMapper mapper,
            IValidator<ProductCategoryAddRequest> addValidator,
            IValidator<ProductCategoryModifyRequest> modifyValidator,
            IValidator<ProductCategoryDeleteRequest> deleteValidator) 
        {
            _repository = repository;
            _mapper = mapper;
            _addValidator = addValidator;
            _modifyValidator = modifyValidator;
            _deleteValidator = deleteValidator;
        }

        public async Task<BaseResponse<ProductCategoryDto>> Handle(ProductCategoryAddRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ProductCategoryDto> { IsSuccess = true };
            var validation = await _addValidator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!validation.IsValid)
            {
                var errors = new StringBuilder();
                validation.Errors.ForEach(x =>
                {
                    errors.AppendLine(x.ErrorMessage);
                });
                response.IsSuccess = false;
                response.Error = Error.ToErrorMessage("BAD_REQUEST", errors.ToString());
                return response;
            }

            var entity = _mapper.Map<ProductCategory>(request.ProductCategory);
            var result = await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            response.Data = _mapper.Map<ProductCategoryDto>(result);

            return response;
        }

        public async Task<BaseResponse<ProductCategoryDto>> Handle(ProductCategoryModifyRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ProductCategoryDto> { IsSuccess = true };
            var validation = await _modifyValidator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!validation.IsValid)
            {
                var errors = new StringBuilder();
                validation.Errors.ForEach(x =>
                {
                    errors.AppendLine(x.ErrorMessage);
                });
                response.IsSuccess = false;
                response.Error = Error.ToErrorMessage("BAD_REQUEST", errors.ToString());
                return response;
            }

            var entity = _mapper.Map<ProductCategory>(request.ProductCategory);
            var result = await _repository.UpdateAsync(entity, entity.Id, cancellationToken).ConfigureAwait(false);
            response.Data = _mapper.Map<ProductCategoryDto>(result);

            return response;
        }

        public async Task<EntityDeleteResponse> Handle(ProductCategoryDeleteRequest request, CancellationToken cancellationToken)
        {
            var response = new EntityDeleteResponse { IsSuccess = true };
            var validation = await _deleteValidator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!validation.IsValid)
            {
                var errors = new StringBuilder();
                validation.Errors.ForEach(x =>
                {
                    errors.AppendLine(x.ErrorMessage);
                });
                response.IsSuccess = false;
                response.Message = errors.ToString();
                return response;
            }

            var result = await _repository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
            response.Message = string.Format(ValidationMessageConstants.DeleteSuccessMessage, "Product Category");

            return response;
        }

        public async Task<BaseResponse<ProductCategoryDto>> Handle(ProductCategoryDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ProductCategoryDto> { IsSuccess = true };
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                response.Error = Error.ToErrorMessage("BAD_REQUEST", string.Format(ValidationMessageConstants.EntityNotFoundMessage, "Product Category"));
                response.IsSuccess = false;
                return response;
            }

            response.Data = _mapper.Map<ProductCategoryDto>(entity);
            return response;
        }
    }
}
