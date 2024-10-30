using Microsoft.EntityFrameworkCore;
using CRM.Persistence.DbContexts;
using CRM.Persistence.IRepositories;
using CRM.Persistence.Repositories;
using CRM.Application.IServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CRMRelationalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CRMRelationalContext") ?? throw new InvalidOperationException("Connection string 'CRMRelationalContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register repository services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); // Replace with your actual repository interfaces and implementations
builder.Services.AddScoped<ICustomerService, CustomerService>(); // Replace with your actual repository interfaces and implementations


// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); // Assuming your AutoMapper profiles are in the same assembly

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customers}/{action=Index}/{id?}");

app.Run();
