namespace DapperDemo.Api.Endpoints.Product.GetProducts;

public static class GetProductRequestHandler
{
    public static async Task<IResult> HandleAsync(IDbConnection dbConnection)
    {
        const string query = """
                           SELECT 
                               p.id AS Id,
                               p.name AS Name,
                               c.name AS CategoryName,
                               p.price AS Price
                           FROM products p
                           JOIN product_categories c ON p.category_id = c.id
                           """;

        var products = await dbConnection.QueryAsync<ProductListViewDto>(query);
        
        return Results.Ok(products);
    }
}