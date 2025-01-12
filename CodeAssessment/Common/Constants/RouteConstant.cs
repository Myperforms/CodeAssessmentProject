namespace CodeAssessment.Common.Constants
{
    public static class RouteConstant
    {
        public const string CommonRoute = "api/[controller]";
        public const string ProductRoute = "api";
        public const string ProductGetRoute = "Products";
        public const string ProductGetByIdRoute = "Products/{productId}";
        public const string ProductDeleteRoute = "Products/{productId}";
        public const string ProductInsertRoute = "Products";
        public const string ProductUpdateRoute = "Products/{productId}";
        public const string ProductAddStockRoute = "Products/add-to-stock/{productId}/{quantity}";
        public const string ProductDecrementStockRoute = "Products/decrement-stock/{productId}/{quantity}";

    }
}
