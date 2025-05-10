using MediatR;
using AutoMapper;
using UniversityApp.Application.Features.Products.Handlers;
using UniversityApp.Web.Mappings;
using UniversityApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args); // ✅ REQUIRED FIRST LINE

builder.Services.AddControllersWithViews();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly));

builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

builder.Services.AddInfrastructure("Data Source=universityapp.db");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
