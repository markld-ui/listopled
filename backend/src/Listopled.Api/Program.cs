var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

Listopled.Application.DependencyInjection.AddApplicationPlaceholder();
Listopled.Infrastructure.DependencyInjection.AddInfrastructurePlaceholder();

var app = builder.Build();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

public partial class Program;
