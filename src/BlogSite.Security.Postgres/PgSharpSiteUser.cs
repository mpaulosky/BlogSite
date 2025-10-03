using System.ComponentModel.DataAnnotations;

using BlogSite.Shared.Entities;
using BlogSite.Shared.Interfaces;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Security.Postgres;

public class PgBlogSiteUser : IdentityUser
{

	[PersonalData, Required, MaxLength(50)]
	public required string DisplayName { get; set; }

	public static explicit operator BlogSiteUser(PgBlogSiteUser user) =>
			new(user.Id, user.DisplayName, user.UserName, user.Email);

	public static explicit operator PgBlogSiteUser(BlogSiteUser user) =>
		new()
		{
			Id = user.Id,
			DisplayName = user.DisplayName,
			UserName = user.UserName,
			Email = user.Email
		};

}

public class PgSecurityContext : IdentityDbContext<PgBlogSiteUser>
{
	public PgSecurityContext(DbContextOptions<PgSecurityContext> options)
		: base(options)
	{
	}

}