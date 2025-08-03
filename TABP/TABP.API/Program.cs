using TABP.API.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
builder.ConfigureLogging();
var app = builder.Build();
app.ConfigureMiddleware();
app.Run();
public partial class Program { }