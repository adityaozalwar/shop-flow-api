using Microsoft.EntityFrameworkCore;
using ShopFlowAPI.Data;
using ShopFlowAPI.Middlewares;
using ShopFlowAPI.Repository;
using ShopFlowAPI.Repository.IRepository;
using ShopFlowAPI.Services;
using ShopFlowAPI.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => "ShopFlowAPI is running....");

app.Run();