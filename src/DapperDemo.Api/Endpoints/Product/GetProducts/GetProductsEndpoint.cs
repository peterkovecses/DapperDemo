namespace DapperDemo.Api.Endpoints.Product.GetProducts;

public static class GetProductsEndpoint
{
    public static IEndpointRouteBuilder MapGetProducts(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/products", GetProductsRequestHandler.HandleAsync)
            .WithName("GetProducts")
            .WithTags("Products");

        return endpointRouteBuilder;
    }
}