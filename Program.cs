using Scalar.AspNetCore;
using testapi.DBContext;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(); 
builder.Services.AddControllers();

var app = builder.Build();



app.MapControllers();

 app.MapOpenApi();
 app.MapScalarApiReference();

app.Run();

