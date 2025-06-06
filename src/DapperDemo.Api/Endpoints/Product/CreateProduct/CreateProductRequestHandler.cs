namespace DapperDemo.Api.Endpoints.Product.CreateProduct;

public static class CreateProductRequestHandler
{
    public static async Task<IResult> HandleAsync(CreateProductRequest request, IDbConnection dbConnection)
    {
        var parameters = new
        {
            Id = Guid.CreateVersion7(),
            request.Name,
            request.Description,
            request.CategoryId,
            request.Price
        };

        const string spName = "dbo.spCreateProduct";
        await dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
        
        var response = new CreateProductResponse(
            parameters.Id,
            request.Name,
            request.Description,
            request.CategoryId,
            request.Price
        );

        return Results.Created($"/api/products/{response.Id}", response);
    }
}