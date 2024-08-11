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
    public class ProductHandler : IRequestHandler<ProductAddRequest, BaseResponse<ProductDto>>,
                    IRequestHandler<ProductModifyRequest, BaseResponse<ProductDto>>,
                    IRequestHandler<ProductDeleteRequest, EntityDeleteResponse>,
                    IRequestHandler<ProductDetailRequest, BaseResponse<ProductDto>>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductAddRequest> _addValidator;
        private readonly IValidator<ProductModifyRequest> _modifyValidator;
        private readonly IValidator<ProductDeleteRequest> _deleteValidator;

        public ProductHandler(IGenericRepository<Product> repository,
            IMapper mapper,
            IValidator<ProductAddRequest> addValidator,
            IValidator<ProductModifyRequest> modifyValidator,
            IValidator<ProductDeleteRequest> deleteValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _addValidator = addValidator;
            _modifyValidator = modifyValidator;
            _deleteValidator = deleteValidator;
        }

        public async Task<BaseResponse<ProductDto>> Handle(ProductAddRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ProductDto> { IsSuccess = true };
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

            var entity = _mapper.Map<Product>(request.Product);
            var result = await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            response.Data = _mapper.Map<ProductDto>(result);

            return response;
        }

        public async Task<BaseResponse<ProductDto>> Handle(ProductModifyRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ProductDto> { IsSuccess = true };
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

            var entity = _mapper.Map<Product>(request.Product);
            var result = await _repository.UpdateAsync(entity, entity.Id, cancellationToken).ConfigureAwait(false);
            response.Data = _mapper.Map<ProductDto>(result);

            return response;
        }

        public async Task<EntityDeleteResponse> Handle(ProductDeleteRequest request, CancellationToken cancellationToken)
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
            response.Message = string.Format(ValidationMessageConstants.DeleteSuccessMessage, "Product");

            return response;
        }

        public async Task<BaseResponse<ProductDto>> Handle(ProductDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ProductDto> { IsSuccess = true };
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                response.IsSuccess = false;
                response.Error = Error.ToErrorMessage("BAD_REQUEST", string.Format(ValidationMessageConstants.EntityNotFoundMessage, "Product"));
                return response;
            }

            response.Data = _mapper.Map<ProductDto>(entity);
            return response;
        }
    }
}
