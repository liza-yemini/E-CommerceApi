using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CartsDbSettings>(
    builder.Configuration.GetSection("CartsDb"));
builder.Services.Configure<CategoriesDbSettings>(
    builder.Configuration.GetSection("CategoriesDb"));
builder.Services.Configure<BuyersDbSettings>(
    builder.Configuration.GetSection("ClientsDb"));
builder.Services.Configure<OrdersDbSettings>(
    builder.Configuration.GetSection("OrdersDb"));
builder.Services.Configure<ProductsDbSettings>(
    builder.Configuration.GetSection("ProductsDb"));
builder.Services.Configure<SellersDbSettings>(
    builder.Configuration.GetSection("SellersDb"));

builder.Services.AddControllers
    (options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).
    AddJsonOptions(options => 
        options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSingleton<MongoCartsService>();
builder.Services.AddSingleton<MongoCategoriesService>();
builder.Services.AddSingleton<MongoBuyersService>();
builder.Services.AddSingleton<MongoOrdersService>();
builder.Services.AddSingleton<MongoProductsService>();
builder.Services.AddSingleton<MongoSellersService>();


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
