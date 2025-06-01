namespace DapperDemo.Api.Endpoints.Product.CreateProduct;

public record CreateProductResponse(
    Guid Id,
    string Name,
    string Description,
    Guid CategoryId,
    decimal Price
);