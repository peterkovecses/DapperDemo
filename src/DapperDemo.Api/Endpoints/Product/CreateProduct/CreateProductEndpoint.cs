namespace DapperDemo.Api.Endpoints.Product.CreateProduct;

public static class CreateProductEndpoint
{
    public static IEndpointRouteBuilder MapCreateProduct(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/products", CreateProductCommand.HandleAsync)
            .WithName("CreateProduct")
            .WithTags("Products")
            .AddEndpointFilter<ValidationFilter<CreateProductRequest>>();

        return endpoints;
    }
}