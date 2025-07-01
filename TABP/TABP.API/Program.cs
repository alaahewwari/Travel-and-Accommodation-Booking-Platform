using TABP.API.Configurations;
using TABP.API.Extensions;
using TABP.API.Mapping;
using TABP.Application;
using TABP.Infrastructure;
using TABP.Persistence;
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddPersistenceServices(configuration);
builder.Services.AddSwagger();
builder.Services.AddControllers();
builder.Services.AddScoped<IdentityMapper>();
builder.Services.AddControllers();
builder.Services.AddApiValidation();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();