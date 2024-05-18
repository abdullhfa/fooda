using System.Security.Claims;
using CodeFood.Repository;
using CodeFood.Repository.EntityFramework;
using CodeFood.Repository.Interfaces;
using CodeFood.Service.Data;
using CodeFood.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind("Project", new Config());

builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(
    db => db.UseSqlServer(Config.ConnectionString!)
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "admin");
    });

    opts.AddPolicy("UserPolicy", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "user", "admin");
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();