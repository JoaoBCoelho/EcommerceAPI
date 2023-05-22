using EcommerceAPI.Infra.IoC;
using EcommerceAPI.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
#else
    var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
#endif

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce API", Version = "v1" });
});
builder.Services.AddVersioning();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddAutoMapper();
builder.Services.RegisterDbContext(configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
