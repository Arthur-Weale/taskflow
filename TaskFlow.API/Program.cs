using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=taskflow.db"));

// Allow Blazor frontend to call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorClient", policy =>
        policy.WithOrigins("https://localhost:7000", "http://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod());
});

// Register Swagger services ← THIS WAS MISSING
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Swagger is a tool that helps us document and test our API. It provides a nice UI where we can see all the endpoints, their parameters, and even try them out directly from the browser.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("BlazorClient");
app.UseAuthorization();
app.MapControllers();

app.Run();