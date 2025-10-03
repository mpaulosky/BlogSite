namespace BlogSite.Security.Postgres;

internal sealed class IdentityUserAccessor(UserManager<PgBlogSiteUser> userManager, IdentityRedirectManager redirectManager)
{
	public async Task<PgBlogSiteUser> GetRequiredUserAsync(HttpContext context)
	{
		var user = await userManager.GetUserAsync(context.User);

		if (user is null)
		{
			redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
		}

		return user;
	}
}