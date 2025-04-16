using MudBlazor.Services; // Add this using directive to resolve AddMudServices
using SunriseSunsetApp.Components;
using SunriseSunsetApp.Middleware;
using SunriseSunsetApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
   .AddInteractiveServerComponents();

builder.Services.AddMudServices(); // This now works after adding the correct namespace
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ClientInfoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.UseMiddleware<ClientIpMiddleware>();

app.Run();
