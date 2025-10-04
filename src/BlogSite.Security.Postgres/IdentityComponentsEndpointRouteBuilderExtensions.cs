// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     IdentityComponentsEndpointRouteBuilderExtensions.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Security.Postgres
// =======================================================

using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

using BlogSite.Security.Postgres;
using BlogSite.Security.Postgres.Account.Pages;
using BlogSite.Security.Postgres.Account.Pages.Manage;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.Routing;

internal static class IdentityComponentsEndpointRouteBuilderExtensions
{

	// These endpoints are required by the Identity Razor components defined in the /Components/Account/Pages directory of this project.
	public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
	{
		ArgumentNullException.ThrowIfNull(endpoints);

		RouteGroupBuilder accountGroup = endpoints.MapGroup("/Account");

		accountGroup.MapPost("/PerformExternalLogin", (
				HttpContext context,
				[FromServices] SignInManager<PgBlogSiteUser> signInManager,
				[FromForm] string provider,
				[FromForm] string returnUrl) =>
		{
			IEnumerable<KeyValuePair<string, StringValues>> query =
			[
					new("ReturnUrl", returnUrl),
					new("Action", ExternalLogin.LoginCallbackAction)
			];

			string redirectUrl = UriHelper.BuildRelative(
					context.Request.PathBase,
					"/Account/ExternalLogin",
					QueryString.Create(query));

			AuthenticationProperties properties =
					signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

			return TypedResults.Challenge(properties, [provider]);
		});

		accountGroup.MapPost("/Logout", async (
				ClaimsPrincipal user,
				[FromServices] SignInManager<PgBlogSiteUser> signInManager,
				[FromForm] string returnUrl) =>
		{
			await signInManager.SignOutAsync();

			return TypedResults.LocalRedirect($"~/{returnUrl}");
		});

		RouteGroupBuilder manageGroup = accountGroup.MapGroup("/Manage").RequireAuthorization();

		manageGroup.MapPost("/LinkExternalLogin", async (
				HttpContext context,
				[FromServices] SignInManager<PgBlogSiteUser> signInManager,
				[FromForm] string provider) =>
		{
			// Clear the existing external cookie to ensure a clean login process
			await context.SignOutAsync(IdentityConstants.ExternalScheme);

			string redirectUrl = UriHelper.BuildRelative(
					context.Request.PathBase,
					"/Account/Manage/ExternalLogins",
					QueryString.Create("Action", ExternalLogins.LinkLoginCallbackAction));

			AuthenticationProperties properties =
					signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl,
							signInManager.UserManager.GetUserId(context.User));

			return TypedResults.Challenge(properties, [provider]);
		});

		ILoggerFactory loggerFactory = endpoints.ServiceProvider.GetRequiredService<ILoggerFactory>();
		ILogger downloadLogger = loggerFactory.CreateLogger("DownloadPersonalData");

		manageGroup.MapPost("/DownloadPersonalData", async (
				HttpContext context,
				[FromServices] UserManager<PgBlogSiteUser> userManager,
				[FromServices] AuthenticationStateProvider authenticationStateProvider) =>
		{
			PgBlogSiteUser? user = await userManager.GetUserAsync(context.User);

			if (user is null)
			{
				return Results.NotFound($"Unable to load user with ID '{userManager.GetUserId(context.User)}'.");
			}

			string userId = await userManager.GetUserIdAsync(user);
			downloadLogger.LogInformation("User with ID '{UserId}' asked for their personal data.", userId);

			// Only include personal data for download
			Dictionary<string, string> personalData = new ();

			IEnumerable<PropertyInfo> personalDataProps = typeof(PgBlogSiteUser).GetProperties()
					.Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

			foreach (PropertyInfo p in personalDataProps)
			{
				personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
			}

			IList<UserLoginInfo> logins = await userManager.GetLoginsAsync(user);

			foreach (UserLoginInfo l in logins)
			{
				personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
			}

			personalData.Add("Authenticator Key", (await userManager.GetAuthenticatorKeyAsync(user))!);
			byte[] fileBytes = JsonSerializer.SerializeToUtf8Bytes(personalData);

			context.Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");

			return TypedResults.File(fileBytes, "application/json", "PersonalData.json");
		});

		return accountGroup;
	}

}