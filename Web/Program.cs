using Microsoft.EntityFrameworkCore;
using Infra.PurchaseContext.Repositories;
using Infra.PurchaseContext.Configuration;
using Infra.PostalCodeContext.Repositories;
using Domain.PurchaseContext.Services;
using Domain.PurchaseContext.Entities;
using Domain.PurchaseContext.Interfaces.Services;
using Domain.PurchaseContext.Interfaces.Repositories;
using Domain.PostalCodeContext.Services;
using Domain.PostalCodeContext.Interfaces.Repositories;
using Domain.PostalCodeContext.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ContextBase>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddDefaultIdentity<User>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();

// INTERFACE AND REPOSITORY

builder.Services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<ICompaniesRepository, CompanyRepository>();
builder.Services.AddSingleton<ISuppliersRepository, SupplierRepository>();
builder.Services.AddSingleton<IPostalCodeRepository, PostalCodeRepository>();

// Services

builder.Services.AddSingleton<ICompanyService, CompanyService>();
builder.Services.AddSingleton<IPostalCodeService, PostalCodeService>();

IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

// Adiciona a instância IConfiguration no container de DI
builder.Services.AddSingleton(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
