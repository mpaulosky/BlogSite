// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     UserRepository.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Security.Postgres
// =======================================================

using System.Security.Claims;

using BlogSite.Shared.Entities;
using BlogSite.Shared.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSite.Security.Postgres;

public class UserRepository
(
		IServiceProvider services
) : IUserRepository
{

	private BlogSiteUser CurrentUser = null!;

	public async Task<BlogSiteUser> GetUserAsync(ClaimsPrincipal user)
	{

		if (CurrentUser is null)
		{

			using IServiceScope scope = services.CreateScope();
			UserManager<PgBlogSiteUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<PgBlogSiteUser>>();

			PgBlogSiteUser? pgUser = await userManager.GetUserAsync(user);

			if (pgUser is null)
			{
				return null!;
			}

			CurrentUser = (BlogSiteUser)pgUser;
		}

		return CurrentUser;

	}

	public async Task<IEnumerable<BlogSiteUser>> GetAllUsersAsync()
	{
		using IServiceScope scope = services.CreateScope();
		PgSecurityContext userManager = scope.ServiceProvider.GetRequiredService<PgSecurityContext>();

		List<BlogSiteUser> pgUsers = await userManager.Users
				.GroupJoin(userManager.UserRoles, u => u.Id, ur => ur.UserId, (u, urs) => new { u, urs })
				.SelectMany(
						x => x.urs.DefaultIfEmpty(),
						(x, ur) => new { x.u, ur }
				)
				.GroupJoin(userManager.Roles, x => x.ur!.RoleId, r => r.Id, (x, rs) => new { x.u, x.ur, rs })
				.SelectMany(
						x => x.rs.DefaultIfEmpty(),
						(x, r) => new BlogSiteUser(x.u.Id, x.u.DisplayName, x.u.UserName, x.u.Email)
						{
								Role = r != null ? r.Name : "No Role Assigned"
						}
				).ToListAsync();

		return pgUsers;
	}

	public async Task UpdateRoleForUserAsync(BlogSiteUser user)
	{

		if (user is null)
		{
			return;
		}

		using IServiceScope scope = services.CreateScope();
		UserManager<PgBlogSiteUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<PgBlogSiteUser>>();

		PgBlogSiteUser? existingUser = userManager.Users.FirstOrDefault(u => u.Id == user.Id);

		if (existingUser is null)
		{
			return;
		}

		string? existingRole = (await userManager.GetRolesAsync(existingUser)).FirstOrDefault();

		if (existingRole is not null)
		{
			await userManager.RemoveFromRoleAsync(existingUser, existingRole);
		}

		if (user.Role is not null)
		{
			await userManager.AddToRoleAsync(existingUser, user.Role);
		}


	}

}