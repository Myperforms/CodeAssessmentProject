using CodeAssessment.Application.Features.Products.Commands;
using CodeAssessment.Application.Features.Products.Commands.CreateProduct;
using CodeAssessment.Application.Features.Products.Commands.DeleteProduct;
using CodeAssessment.Application.Features.Products.Commands.StockUpdate;
using CodeAssessment.Application.Features.Products.Commands.UpdateProduct;
using CodeAssessment.Application.Features.Products.Models;
using CodeAssessment.Application.Features.Products.Queries;
using CodeAssessment.Application.Response;
using CodeAssessment.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeAssessment.Controllers
{
    [Route(RouteConstant.ProductRoute)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            this._logger = logger;
            this._mediator = mediator;
        }


        [HttpGet(RouteConstant.ProductGetRoute)]
        public async Task<IActionResult> GetProducts()
        {
            this._logger.LogInformation($"Product Controller >> GetProducts() > GET[] > ");

            var response = await this._mediator.Send(new GetProductQuery());

            return ReturnResponse(response);
        }


        [HttpGet(RouteConstant.ProductGetByIdRoute)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            this._logger.LogInformation($"Product Controller >> GetProductById() > GET[] > productId:  {productId}");

            var response = await this._mediator.Send(new GetProductQuery() { ProductId = productId });

            return ReturnResponse(response);

        }

        [HttpDelete(RouteConstant.ProductDeleteRoute)]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            this._logger.LogInformation($"Product Controller >> ProductDelete() > Delete[] > productId:  {productId}");

            var response = await this._mediator.Send(new DeleteProductCommand() { ProductId = productId });

            return ReturnResponse(response);
        }

        [HttpPost(RouteConstant.ProductInsertRoute)]
        public async Task<IActionResult> ProductInsert([FromBody] ProductDetails model)
        {
            this._logger.LogInformation($"Product Controller >> ProductInsert() > POST[]");

            var productDetails = new CreateProductCommand()
            {
                ProductDescrption = model.ProductDescrption,
                ProductName = model.ProductName,
                Amount = model.Amount,
                Quantity = model.Quantity,
                SearchKeyWord = model.SearchKeyWord,
            };
            var response = await this._mediator.Send(productDetails);

            return ReturnResponse(response);
        }

        [HttpPut(RouteConstant.ProductUpdateRoute)]
        public async Task<IActionResult> ProductUpdate(int productId, [FromBody] ProductDetails model)
        {
            this._logger.LogInformation($"Product Controller >> ProductUpdate() > PUT[] > productId:  {productId}");

            var productDetails = new UpdateProductCommand()
            {
                ProductId = productId,
                ProductDescrption = model.ProductDescrption,
                ProductName = model.ProductName,
                Amount = model.Amount,
                Quantity = model.Quantity,
                SearchKeyWord = model.SearchKeyWord,
            };
            var response = await this._mediator.Send(productDetails);

            return ReturnResponse(response);
        }

        [HttpPut(RouteConstant.ProductDecrementStockRoute)]
        public async Task<IActionResult> ProductDecrementQuantity(int productId, int quantity)
        {
            this._logger.LogInformation($"Product Controller >> ProductDecrementQuantity() > PUT[] > productId:  {productId}");


            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = productId,
                Quantity = quantity,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.DecrementQuantity
            };

            var response = await this._mediator.Send(productDetails);

            return ReturnResponse(response);
        }

        [HttpPut(RouteConstant.ProductAddStockRoute)]
        public async Task<IActionResult> ProductAddQuantity(int productId, int quantity)
        {
            this._logger.LogInformation($"Product Controller >> ProductAddQuantity() > PUT[] > productId:  {productId}");

            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = productId,
                Quantity = quantity,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.AddQuantity
            };

            var response = await this._mediator.Send(productDetails);

            return ReturnResponse(response);
        }

        private IActionResult ReturnResponse(BaseResponse<object> baseResponse)
        {
            return !baseResponse.HasError ? Ok(baseResponse.Data) : StatusCode(baseResponse.Code, baseResponse);
        }

    }
}
