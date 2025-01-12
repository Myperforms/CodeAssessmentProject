using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using CodeAssessment.Application.Features.Products.Models;
using CodeAssessment.Application.Response;
using CodeAssessment.Application.Services;
using CodeAssessment.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodeAssessment.Application.Features.Products.Commands.StockUpdate
{
    public class UpdateProductStockCommandHandler : IRequestHandler<UpdateProductStockCommand, BaseResponse<object>>
    {
        private readonly ILogger<UpdateProductStockCommandHandler> _logger;
        private readonly IProductRepository _productRepository;

        public UpdateProductStockCommandHandler(ILogger<UpdateProductStockCommandHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<BaseResponse<object>> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var responseModel = new BaseResponse<object>();
            try
            {
                if (request.ProductId == 0 || request.Quantity == 0)
                {
                    return CommonHandlers.InvalidRequestHandler("Missing Product Id or Product Quantity.");
                }

                var productDetails = _productRepository.GetById(request.ProductId);
                if (productDetails == null)
                {
                    return CommonHandlers.InvalidRequestHandler("Product not exists.");
                }

                if (request.Type == Common.Enums.ProductStockUpdateType.AddQuantity)
                {
                    return AddStock(request, productDetails);
                }

                if (request.Type == Common.Enums.ProductStockUpdateType.DecrementQuantity)
                {
                    return DecrementStock(request, productDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateProductStockCommandHandler() > Exception: {ex.Message}");

                responseModel = CommonHandlers.ExceptionHandler();
            }
            return responseModel;
        }

        private BaseResponse<object> AddStock(UpdateProductStockCommand request, Domain.Entities.Product productDetails)
        {
            productDetails.Quantity += request.Quantity;
            _productRepository.Update(productDetails);
            _productRepository.SaveChanges();

            var responseModel = new BaseResponse<object>()
            {
                Code = StatusCodes.Status204NoContent,
                Data = $"Product stock updated. now available stock is {productDetails.Quantity}",
            };
            return responseModel;
        }

        private BaseResponse<object> DecrementStock(UpdateProductStockCommand request, Domain.Entities.Product productDetails)
        {
            var responseModel = new BaseResponse<object>();

            if (productDetails.Quantity >= request.Quantity)
            {
                productDetails.Quantity -= request.Quantity;
                _productRepository.Update(productDetails);
                _productRepository.SaveChanges();
                responseModel.Data = $"Product stock decrement completed. now available Stock is ({productDetails.Quantity})";
            }
            else
            {
                responseModel.Data = $"not able to update. because we have only {productDetails.Quantity} available Quanity for the product.";
            }

            responseModel.Code = StatusCodes.Status204NoContent;


            return responseModel;
        }
    }
}
