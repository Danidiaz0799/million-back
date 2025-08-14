using RealEstate.Api.Configurations;
using RealEstate.Api.Middleware;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Enhanced Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Real Estate API",
        Version = "v1",
        Description = "API para gestión de propiedades inmobiliarias",
        Contact = new OpenApiContact
        {
            Name = "Real Estate Team",
            Email = "support@realestate.com"
        }
    });

    // Include XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Add CORS - More permissive configuration for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCORS", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
    
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://localhost:5064")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Add project services
builder.Services.AddProjectServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate API v1");
        c.RoutePrefix = "swagger"; // Swagger UI will be at /swagger
    });
}

app.UseHttpsRedirection();

// Use CORS - More permissive for development
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCORS");
}
else
{
    app.UseCors("AllowReactApp");
}

app.UseAuthorization();

// Use custom exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();

// Make the implicit Program class public so it can be referenced by tests
public partial class Program { }
