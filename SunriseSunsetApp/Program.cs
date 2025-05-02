using MudBlazor.Services;
using SunriseSunsetApp.Components;
using SunriseSunsetApp.Data;
using SunriseSunsetApp.Middleware;
using SunriseSunsetApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddRazorComponents()
   .AddInteractiveServerComponents();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AspNetCore.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
});

builder.Services.AddMudServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ClientInfoService>();
builder.Services.AddScoped<SessionTracker>();
builder.Services.AddScoped<Database>();

var app = builder.Build();

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseMiddleware<ClientIpMiddleware>();


app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
