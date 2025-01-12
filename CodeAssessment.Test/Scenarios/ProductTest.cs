using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Application.Features.Products.Commands.CreateProduct;
using CodeAssessment.Application.Features.Products.Commands.DeleteProduct;
using CodeAssessment.Application.Features.Products.Commands.StockUpdate;
using CodeAssessment.Application.Features.Products.Commands.UpdateProduct;
using CodeAssessment.Application.Features.Products.Queries;
using CodeAssessment.Test.Common;
using MediatR;
using Xunit;
using Xunit.Abstractions;

namespace CodeAssessment.Test.Scenarios
{
    public class ProductTest : TestBase
    {
        private readonly IMediator _mediator;
        private readonly ITestOutputHelper output;


        public ProductTest(ServiceLocationSetup serviceLocationSetup, IMediator mediator, ITestOutputHelper output) : base(serviceLocationSetup)
        {
            _mediator = mediator;
        }

        [Fact]
        public  void Product_GetAllProducts_Successful()
        {
            var response =  this._mediator.Send(new GetProductQuery());
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_GetProductById_Successful()
        {
            var getProductQuery = new GetProductQuery() { ProductId = 100000 };
            var response = await this._mediator.Send(getProductQuery);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_ProductDeletewithCorrectProductId_Successful()
        {
            var getProductQuery = new DeleteProductCommand() { ProductId = 100000 };
            var response = await this._mediator.Send(getProductQuery);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_ProductDeleteWrongProductId_Successful()
        {
            var getProductQuery = new DeleteProductCommand() { ProductId = 3345 };
            var response = await this._mediator.Send(getProductQuery);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_AddProduct_Successful()
        {
            var productDetails = new CreateProductCommand()
            {
                ProductName = "T-Shirt",
                ProductDescrption = "T-Shirt Description.",
                Amount = 10.05m,
                Quantity = 10,
                SearchKeyWord = "tshirt",
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }


        [Fact]
        public async void Product_AddProductWithoutValues_Successful()
        {
            var productDetails = new CreateProductCommand()
            {
                ProductName = "",
                ProductDescrption = "",
                Amount = 0,
                Quantity = 0,
                SearchKeyWord = "",
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_updateProduct_Successful()
        {
            var productDetails = new UpdateProductCommand()
            {
                ProductId = 100001,
                ProductName = "T-Shirt",
                ProductDescrption = "T-Shirt Description.",
                Amount = 10.05m,
                Quantity = 10,
                SearchKeyWord = "tshirt",
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }


        [Fact]
        public async void Product_UpdateProductWithoutValues_Successful()
        {
            var productDetails = new UpdateProductCommand()
            {
                ProductId = 100001,
                ProductName = "",
                ProductDescrption = "",
                Amount = 0,
                Quantity = 0,
                SearchKeyWord = "",
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_AddStock_Successful()
        {
            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = 100001,
                Quantity = 10,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.AddQuantity,
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_AddStockWrongProductId_Successful()
        {
            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = 32341,
                Quantity = 10,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.AddQuantity,
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_AddStockWithoutQuantity_Successful()
        {
            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = 100001,
                Quantity = 0,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.AddQuantity,
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_UpdateStock_Successful()
        {
            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = 100001,
                Quantity = 10,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.AddQuantity,
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_UpdateStockWrongProductId_Successful()
        {
            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = 32341,
                Quantity = 10,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.AddQuantity,
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

        [Fact]
        public async void Product_UpdateStockWithoutQuantity_Successful()
        {
            var productDetails = new UpdateProductStockCommand()
            {
                ProductId = 100001,
                Quantity = 0,
                Type = CodeAssessment.Common.Enums.ProductStockUpdateType.AddQuantity,
            };

            var response = await this._mediator.Send(productDetails);
            Assert.NotNull(response);
        }

    }
}
