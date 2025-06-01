namespace DapperDemo.Api.Endpoints.Product.GetProducts;

public record ProductListViewDto(
    Guid Id,
    string Name,
    string CategoryName,
    decimal Price);