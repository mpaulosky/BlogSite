// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     RegisterPostgresSecurityServices.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Security.Postgres
// =======================================================

global using Microsoft.AspNetCore.Components;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Logging;

using System.Diagnostics;

using BlogSite.Shared;
using BlogSite.Shared.Entities;
using BlogSite.Shared.Interfaces;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogSite.Security.Postgres;

public class RegisterPostgresSecurityServices : IRegisterServices, IRunAtStartup
{

	private const string InitializeUsersActivitySourceName = "Initial Users and Roles";

	public IHostApplicationBuilder RegisterServices(IHostApplicationBuilder builder, bool disableRetry = false)
	{

		builder.Services.AddCascadingAuthenticationState();
		builder.Services.AddScoped<IdentityUserAccessor>();
		builder.Services.AddScoped<IdentityRedirectManager>();
		builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

		builder.Services.AddScoped<IUserRepository, UserRepository>();

		builder.Services.AddAuthentication(options =>
				{
					options.DefaultScheme = IdentityConstants.ApplicationScheme;
					options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
				})
				.AddIdentityCookies();

		ConfigurePostgresDbContext(builder, disableRetry);

		builder.Services.AddIdentityCore<PgBlogSiteUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<PgSecurityContext>()
				.AddSignInManager()
				.AddDefaultTokenProviders();

		builder.Services.AddOpenTelemetry()
				.WithTracing(tracing => tracing.AddSource(InitializeUsersActivitySourceName));


		builder.Services.AddSingleton<IEmailSender<PgBlogSiteUser>, IdentityNoOpEmailSender>();

		return builder;

	}

	public async Task RunAtStartup(IServiceProvider services)
	{

		ActivitySource activitySource = new (InitializeUsersActivitySourceName);
		Activity? activity = activitySource.CreateActivity("Inspecting roles", ActivityKind.Internal);

		using IServiceScope scope = services.CreateScope();
		IServiceProvider provider = scope.ServiceProvider;

		activity?.Start();
		RoleManager<IdentityRole> roleMgr = provider.GetRequiredService<RoleManager<IdentityRole>>();
		bool adminExists = await roleMgr.RoleExistsAsync(Roles.Admin);

		if (!adminExists)
		{
			await roleMgr.CreateAsync(new IdentityRole(Roles.Admin));
			activity?.AddEvent(new ActivityEvent("Created Admin role"));
		}

		bool editorExists = await roleMgr.RoleExistsAsync(Roles.Author);

		if (!editorExists)
		{
			await roleMgr.CreateAsync(new IdentityRole(Roles.Author));
			activity?.AddEvent(new ActivityEvent("Created Editor role"));
		}

		bool userExists = await roleMgr.RoleExistsAsync(Roles.User);

		if (!userExists)
		{
			await roleMgr.CreateAsync(new IdentityRole(Roles.User));
			activity?.AddEvent(new ActivityEvent("Created User role"));
		}

		activity?.Stop();

		activity = activitySource.CreateActivity("Inspecting users", ActivityKind.Internal);
		activity?.Start();

		UserManager<PgBlogSiteUser> userManager = provider.GetRequiredService<UserManager<PgBlogSiteUser>>();
		bool anyUsers = await userManager.Users.AnyAsync();

		if (!anyUsers)
		{
			PgBlogSiteUser admin = new()
			{
					DisplayName = "Admin", UserName = "admin@localhost", Email = "admin@localhost", EmailConfirmed = true
			};

			IdentityResult newUserResult = await userManager.CreateAsync(admin, "Admin123!");
			activity?.AddEvent(new ActivityEvent("Created admin user with password 'Admin123!'"));
			await userManager.AddToRoleAsync(admin, Roles.Admin);
			activity?.AddEvent(new ActivityEvent("Assigned admin user to Admin role"));
		}

	}

	public static void ConfigurePostgresDbContext(IHostApplicationBuilder builder, bool disableRetry)
	{
		builder.AddNpgsqlDbContext<PgSecurityContext>(Services.UserDatabase, configure =>
		{
			configure.DisableRetry = disableRetry;
		}, configure =>
		{
			configure.UseNpgsql(options =>
			{
				options.MigrationsHistoryTable("__EFMigrationsHistory_Security");
			});
		});
	}

	public void MapEndpoints(IEndpointRouteBuilder endpointDooHickey)
	{
		endpointDooHickey.MapAdditionalIdentityEndpoints();
	}

}