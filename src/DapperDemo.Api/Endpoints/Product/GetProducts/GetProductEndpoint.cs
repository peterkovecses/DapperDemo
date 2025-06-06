namespace DapperDemo.Api.Endpoints.Product.GetProducts;

public static class GetProductEndpoint
{
    public static IEndpointRouteBuilder MapGetProducts(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/products", GetProductRequestHandler.HandleAsync)
            .WithName("GetProducts")
            .WithTags("Products");

        return endpointRouteBuilder;
    }
}