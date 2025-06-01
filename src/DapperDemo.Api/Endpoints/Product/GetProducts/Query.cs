namespace DapperDemo.Api.Endpoints.Product.GetProducts;

public static class Query
{
    public static async Task<IResult> HandleAsync(IDbConnection dbConnection)
    {
        const string sql = """
                           SELECT 
                               p.id AS Id,
                               p.name AS Name,
                               c.name AS CategoryName,
                               p.price AS Price
                           FROM products p
                           JOIN product_categories c ON p.category_id = c.id
                           """;

        IEnumerable<ProductListViewDto> products;
        
        try
        {
            products = await dbConnection.QueryAsync<ProductListViewDto>(sql);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return Results.Ok(products);
    }
}