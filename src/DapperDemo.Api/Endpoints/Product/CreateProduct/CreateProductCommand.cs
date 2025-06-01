namespace DapperDemo.Api.Endpoints.Product.CreateProduct;

public static class CreateProductCommand
{
    public static async Task<IResult> HandleAsync(CreateProductRequest request, IDbConnection dbConnection)
    {
        var parameters = new
        {
            Id = Guid.NewGuid(),
            request.Name,
            request.Description,
            request.CategoryId,
            request.Price
        };

        const string spName = "dbo.spCreateProduct";
        await dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        
        return Results.Created($"/api/products/{parameters.Id}", parameters);
    }
}