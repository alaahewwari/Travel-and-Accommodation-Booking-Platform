using TABP.API.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.ConfigureLogging();
var configuration = ConfigurationBuilderHelper.BuildConfiguration();
//builder.Services.Configure<JwtConfigurations>(configuration.GetSection(JwtConfigurations.SectionName));
builder.Services.AddLayerServices(configuration);
builder.Services.AddApiServices();
builder.Services.AddPipelineBehaviors();
var app = builder.Build();
app.ConfigureMiddleware();
app.Run();
public partial class Program { }