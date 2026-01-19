var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.ConfigureLogging();
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.MapEndpoints();
app.Run();