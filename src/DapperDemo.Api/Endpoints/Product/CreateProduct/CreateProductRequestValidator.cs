namespace DapperDemo.Api.Endpoints.Product.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty.")
            .MaximumLength(100)
            .WithMessage("Description must be at most 100 characters.");

        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty)
            .WithMessage("CategoryId must be a valid GUID.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
    }
}