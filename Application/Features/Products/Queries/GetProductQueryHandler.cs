using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Application.Features.Products.Models;
using CodeAssessment.Application.Response;
using CodeAssessment.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AutoMapper;
using CodeAssessment.Application.Services;

namespace CodeAssessment.Application.Features.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, BaseResponse<object>>
    {
        private readonly ILogger<GetProductQueryHandler> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(ILogger<GetProductQueryHandler> logger, IMapper mapper, IProductRepository productRepository)
        {
            this._logger = logger;
            this._productRepository = productRepository;
            this._mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var responseModel = new BaseResponse<object>();

            try
            {
                if (request.ProductId > 0)
                {
                    responseModel.Code = StatusCodes.Status200OK;

                    var productModelCommand = new ProductModel();

                    var productsList = this._productRepository.GetById(request.ProductId);
                    if (productsList != null)
                    {
                        productModelCommand = this._mapper.Map<ProductModel>(productsList);
                        responseModel.Message = "Success";
                        responseModel.Data = productModelCommand;
                    }
                    else
                    {
                        responseModel.Data = "Product not Exists.";
                    }
                }
                else
                {
                    var productModelCommand = new List<ProductModel>();

                    var productsList = this._productRepository.GetAllProducts();
                    if (productsList != null)
                    {
                        productModelCommand = this._mapper.Map<List<ProductModel>>(productsList);
                    }

                    responseModel.Code = StatusCodes.Status200OK;
                    responseModel.Message = "Success";
                    responseModel.Data = productModelCommand;
                }

            }
            catch (Exception ex)
            {
                this._logger.LogError($"CreateCategoryToProductsCommandHandler() > Exception: {ex.Message}");

                responseModel = CommonHandlers.ExceptionHandler();
            }

            return responseModel;
        }
    }
}
