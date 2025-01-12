using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodeAssessment.Application.Features.Products.Models;
using CodeAssessment.Application.Features.Products.Queries;
using CodeAssessment.Application.Response;
using CodeAssessment.Application.Services;
using CodeAssessment.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodeAssessment.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse<object>>
    {
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<object>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var responseModel = new BaseResponse<object>();

            try
            {
                if (request.ProductId == 0)
                {
                    return CommonHandlers.InvalidRequestHandler("ProductId is Required.");
                }

                var productModelCommand = new ProductModel();

                var isProductHasDelete = _productRepository.DeleteProduct(request.ProductId);
                if (isProductHasDelete)
                {
                    responseModel.Code = StatusCodes.Status200OK;
                    responseModel.Data = "Product has been deleted .";
                    _logger.LogInformation($"DeleteProductCommandHandler() > ProductId{request.ProductId}");
                }
                else
                {
                    responseModel.Code = StatusCodes.Status200OK;
                    responseModel.Data = "Product not exist.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteProductCommandHandler() > Exception: {ex.Message}");

                responseModel = CommonHandlers.ExceptionHandler();
            }

            return responseModel;
        }
    }
}
