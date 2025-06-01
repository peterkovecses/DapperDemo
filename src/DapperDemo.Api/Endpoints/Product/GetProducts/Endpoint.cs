namespace DapperDemo.Api.Endpoints.Product.GetProducts;

public static class Endpoint
{
    public static IEndpointRouteBuilder MapGetProducts(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/api/products", Query.HandleAsync)
            .WithName("GetProducts")
            .WithTags("Products");

        return endpointRouteBuilder;
    }
}