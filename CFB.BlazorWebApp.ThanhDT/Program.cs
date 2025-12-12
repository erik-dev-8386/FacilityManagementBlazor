using CFB.BlazorWebApp.ThanhDT.Components;
using CFB.BlazorWebApp.ThanhDT.Services;
using CFB.Services.ThanhDT;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

/// ThanhDT | Add Dependency Injection
builder.Services.AddScoped<FacilityThanhDtService>();
builder.Services.AddScoped<CampusThanhDtService>();
builder.Services.AddScoped<FacilityTypeThanhDtService>();
builder.Services.AddScoped<SystemUserAccountService>();


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Adjust to your login page path
        options.AccessDeniedPath = "/error"; // Adjust as needed
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
