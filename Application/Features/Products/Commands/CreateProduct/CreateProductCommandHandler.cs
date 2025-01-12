using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodeAssessment.Application.Response;
using CodeAssessment.Application.Services;
using CodeAssessment.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodeAssessment.Application.Features.Products.Commands.CreateProduct
{

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse<object>>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var responseModel = new BaseResponse<object>()
            {
                Code = StatusCodes.Status200OK,
            };

            try
            {
                responseModel = CommonHandlers.ValidationHandler(await new CreateProductCommandValidator().ValidateAsync(request));
                if (responseModel.HasError)
                {
                    return responseModel;
                }

                var productDetails = _mapper.Map<Domain.Entities.Product>(request);
                productDetails.CreatedDate = DateTime.Now;

                _productRepository.Insert(productDetails);
                _productRepository.SaveChanges();

                var returnMessage = $"Product Created. ProductId :{productDetails.ProductId}";
                responseModel.Data = returnMessage;

                _logger.LogError($"CreateProductCommandHandler() > Product Created > ProductId : {productDetails.ProductId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateProductCommandHandler() > Exception: {ex.Message}");

                responseModel = CommonHandlers.ExceptionHandler();
            }

            return responseModel;
        }
    }
}
