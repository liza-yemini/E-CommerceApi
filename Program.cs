using Microsoft.AspNetCore.Identity;
using VattaAppApi.Controllers;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<ICartsService, MongoCartsService>();
builder.Services.AddSingleton<ICategoriesService, MongoCategoriesService>();
builder.Services.AddSingleton<ICustomersService, MongoCustomersService>();
builder.Services.AddSingleton<IOrdersService, MongoOrdersService>();
builder.Services.AddSingleton<IProductsService, MongoProductsService>();
builder.Services.AddSingleton<ISellersService, MongoSellersService>();
builder.Services.AddSingleton<IOrderedProductService, MongoOrderedProductService>();

// Add services to the container.
builder.Services.Configure<CartsDbSettings>(
    builder.Configuration.GetSection("CartsDb"));
builder.Services.Configure<CategoriesDbSettings>(
    builder.Configuration.GetSection("CategoriesDb"));
builder.Services.Configure<CustomersDbSettings>(
    builder.Configuration.GetSection("ClientsDb"));
builder.Services.Configure<OrdersDbSettings>(
    builder.Configuration.GetSection("OrdersDb"));
var productsDbSettings = builder.Services.Configure<ProductsDbSettings>(
    builder.Configuration.GetSection("ProductsDb"));
builder.Services.Configure<SellersDbSettings>(
    builder.Configuration.GetSection("SellersDb"));

builder.Services.AddControllers
        (options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).
    AddJsonOptions(options =>
        options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
