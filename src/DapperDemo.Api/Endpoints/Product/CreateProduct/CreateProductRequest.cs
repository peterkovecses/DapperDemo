namespace DapperDemo.Api.Endpoints.Product.CreateProduct;

public record CreateProductRequest(
    string Name, 
    string Description, 
    Guid CategoryId, 
    decimal Price);