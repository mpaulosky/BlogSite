using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlogSite.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

using BlogSite.Shared.Entities;

namespace BlogSite.Security.Postgres;

public class PgBlogSiteUser : IdentityUser
{

	[PersonalData, Required, MaxLength(50)]
	public required string DisplayName { get; set; }

	public static explicit operator BlogSiteUser(PgBlogSiteUser user) =>
			new(user.Id, user.UserName, user.Email)
			{
				DisplayName = user.DisplayName
			};

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