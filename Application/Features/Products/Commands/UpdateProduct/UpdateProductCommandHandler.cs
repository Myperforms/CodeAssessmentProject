using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodeAssessment.Application.Features.Products.Models;
using CodeAssessment.Application.Response;
using CodeAssessment.Application.Services;
using CodeAssessment.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CodeAssessment.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, BaseResponse<object>>
    {
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var responseModel = new BaseResponse<object>();

            try
            {
                if (request.ProductId == 0)
                {
                    return CommonHandlers.InvalidRequestHandler("ProductId is Required");
                }

                responseModel = CommonHandlers.ValidationHandler(await new UpdateProductCommandValidator().ValidateAsync(request));
                if (responseModel.HasError)
                {
                    return responseModel;
                }

                var returnMessage = "";

                var productDetails = _productRepository.GetById(request.ProductId);

                if (productDetails != null)
                {
                    productDetails.ProductDescrption = request.ProductDescrption;
                    productDetails.ProductName = request.ProductName;
                    productDetails.Amount = request.Amount;
                    productDetails.Quantity = request.Quantity;
                    productDetails.SearchKeyWord = request.SearchKeyWord;
                    productDetails.UpdatedDate = DateTime.Now;
                    _productRepository.Update(productDetails);
                    _productRepository.SaveChanges();
                    returnMessage = "Product Details are updated.";
                    _logger.LogError($"UpdateProductCommandHandler() > Product Updated > ProductId : {productDetails.ProductId}");
                }
                else
                {
                    returnMessage = "Product not exists.";
                }

                responseModel.Code = StatusCodes.Status200OK;
                responseModel.Data = returnMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateProductCommandHandler() > Exception: {ex.Message}");

                responseModel = CommonHandlers.ExceptionHandler();
            }

            return responseModel;
        }
    }
}
