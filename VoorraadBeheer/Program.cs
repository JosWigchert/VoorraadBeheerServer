using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using VoorraadBeheer.Authentication;
using VoorraadBeheer.Authentication.Cryptography;
using VoorraadBeheer.Data;
using VoorraadBeheer.Models;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

mySQLSqlHelper.ConnectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<UserDetailService>();
builder.Services.AddSingleton<IngredientDetailService>();
builder.Services.AddSingleton<ProductDetailService>();
builder.Services.AddSingleton<CryptographyHelper>();
builder.Services.AddScoped<ProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
