using IchthusPOSWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Configure the DbContext with retry logic
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,              // Number of retries
            maxRetryDelay: TimeSpan.FromSeconds(10), // Maximum delay between retries
            errorNumbersToAdd: null        // Specific SQL error numbers to retry on
        )
    )
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ichthus Web", Version = "v1" });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins(
            "http://localhost:3000", "http://192.168.10.158:3000","http://192.168.10.222:8020","http://192.168.10.222:8022", "http://localhost:8001", "http://114.29.239.77:8001") //MY PC
          //   builder.WithOrigins("http://localhost:15779", "http://192.168.45.105:15779", "http://161.49.102.243:8020", "http://ichthuspayroll.click:20258", "http://192.168.10.158:3000", "http://localhost:8020", "http://192.168.45.105:8020") //Malabon
          //builder.WithOrigins("http://192.168.10.222:8020", "http://localhost:8020", "http://192.168.10.222:3000/") // Batangas       
                 .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials(); // Ensure credentials are allowed
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ichthus Web V1");
    c.RoutePrefix = "swagger";
    // Collapse all endpoints by default
    c.DefaultModelsExpandDepth(0); // Collapses the models section
    c.DefaultModelExpandDepth(0); // Collapses the individual models in the API documentation
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // Collapses all the endpoints
});

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
