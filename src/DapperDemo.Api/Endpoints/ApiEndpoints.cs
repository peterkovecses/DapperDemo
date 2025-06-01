namespace DapperDemo.Api.Endpoints;

public static class ApiEndpoints
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGetProducts();
        endpointRouteBuilder.MapCreateProduct();
        
        return endpointRouteBuilder;
    }
}