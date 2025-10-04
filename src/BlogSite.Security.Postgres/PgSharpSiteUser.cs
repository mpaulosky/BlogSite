// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgSharpSiteUser.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Security.Postgres
// =======================================================

using System.ComponentModel.DataAnnotations;

using BlogSite.Shared.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogSite.Security.Postgres;

public class PgBlogSiteUser : IdentityUser
{

	[ PersonalData]
	[ Required]
	[ MaxLength(50)]
	public required string DisplayName { get; set; }

	public static explicit operator BlogSiteUser(PgBlogSiteUser user)
	{
		return new BlogSiteUser(user.Id, user.DisplayName, user.UserName, user.Email);
	}

	public static explicit operator PgBlogSiteUser(BlogSiteUser user)
	{
		return new PgBlogSiteUser
		{
				Id = user.Id, DisplayName = user.DisplayName, UserName = user.UserName, Email = user.Email
		};
	}

}

public class PgSecurityContext : IdentityDbContext<PgBlogSiteUser>
{

	public PgSecurityContext(DbContextOptions<PgSecurityContext> options)
			: base(options) { }

}