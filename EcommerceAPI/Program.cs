using EcommerceAPI.Infra.IoC;
using EcommerceAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
#else
    var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
#endif
//var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersioning();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddAutoMapper();
builder.Services.RegisterDbContext(configuration);
builder.Services.RegisterIdentity(configuration);
builder.Services.AddAuthentication(configuration);
builder.Services.AddSwagger();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
