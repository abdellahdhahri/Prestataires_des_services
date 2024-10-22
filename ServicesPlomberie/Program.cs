using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServicesPlomberie.Data;
using ServicesPlomberie.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Utilisateur,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//cloudinary configuration 
// Load Cloudinary settings from appsettings.json.
var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();

// Configure Cloudinary settings.
var account = new Account(
    cloudinarySettings.CloudName,
    cloudinarySettings.ApiKey,
    cloudinarySettings.ApiSecret
);
builder.Services.AddSingleton(account);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
