using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using BlogSite.Shared;
using BlogSite.Data.Postgres;
using BlogSite.Security.Postgres;
using BlogSite.ServiceDefaults;
using BlogSite.Shared.Entities;

var builder = WebApplication.CreateBuilder(args);

// Load postgres
var pg = new RegisterPostgresServices();

pg.RegisterServices(builder);

var pgSecurity = new RegisterPostgresSecurityServices();

pgSecurity.RegisterServices(builder);

var appState = builder.AddPluginManagerAndAppState();

// add the custom localization features for the application framework
builder.ConfigureRequestLocalization();

builder.Services.AddHttpContextAccessor();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Configure larger messages to allow upload of packages
builder.Services.Configure<HubOptions>(options =>
{
	options.EnableDetailedErrors = true;
});

// Add services to the container.
builder.Services.AddRazorComponents()
		.AddInteractiveServerComponents()
		.AddCircuitOptions(o =>
		{
			o.DetailedErrors = true;
		});


builder.Services.AddOutputCache();

builder.Services.AddMemoryCache();

// add an implementation of IEmailSender that does nothing for BlogSiteUser
builder.Services.AddTransient<IEmailSender<BlogSiteUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseOutputCache();

// add error handlers for page not found
app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");

//var pluginManager = await app.ActivatePluginManager(appState);

app.MapRazorComponents<App>()
		.AddInteractiveServerRenderMode()
		.AddAdditionalAssemblies(
		typeof(PgBlogSiteUser).Assembly
		//typeof(Sample.FirstThemePlugin.Theme).Assembly
		);

app.UseAntiforgery();

pgSecurity.MapEndpoints(app);

//app.MapSiteMap();
//app.MapRobotsTxt();
//app.MapRssFeed();
app.MapDefaultEndpoints();

app.UseRequestLocalization();

await pgSecurity.RunAtStartup(app.Services);

//app.MapFileApi(pluginManager);

app.Run();