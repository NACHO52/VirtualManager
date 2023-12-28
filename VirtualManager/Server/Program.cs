using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using System.Data;
using VirtualManager.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

string dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

//My services
builder.Services.AddScoped<ISystemUserDAO, SystemUserDAO>();
builder.Services.AddScoped<IResourceItemDAO, ResourceItemDAO>();
builder.Services.AddScoped<ITaxDAO, TaxDAO>();
builder.Services.AddScoped<IProductDAO, ProductDAO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
