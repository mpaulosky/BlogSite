// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     IdentityUserAccessor.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Security.Postgres
// =======================================================

namespace BlogSite.Security.Postgres;

internal sealed class IdentityUserAccessor
(
		UserManager<PgBlogSiteUser> userManager,
		IdentityRedirectManager redirectManager
)
{

	public async Task<PgBlogSiteUser> GetRequiredUserAsync(HttpContext context)
	{
		PgBlogSiteUser? user = await userManager.GetUserAsync(context.User);

		if (user is null)
		{
			redirectManager.RedirectToWithStatus("Account/InvalidUser",
					$"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
		}

		return user;
	}

}