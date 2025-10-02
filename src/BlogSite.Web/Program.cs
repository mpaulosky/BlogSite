using BlogSite.ServiceDefaults;
using BlogSite.Web;
using BlogSite.Web.Components;
using BlogSite.Shared.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseOutputCache();

// Map static assets before other routing
app.MapStaticAssets();

// Add some logging for development
if (app.Environment.IsDevelopment())
{
    app.Logger.LogInformation("Static assets mapped successfully in Development environment");
}

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Log if running in Dev Container (Rider/VS Code)
if (RuntimeEnvironment.IsRunningInDevContainer())
{
    app.Logger.LogInformation("Running inside a Dev Container (Rider): RIDER_DEVCONTAINER=true");
}
else if (RuntimeEnvironment.IsRunningInContainer())
{
    app.Logger.LogInformation("Running inside a container (non-dev)");
}

app.MapDefaultEndpoints();

app.Run();