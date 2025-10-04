// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     IUserRepository.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

using System.Security.Claims;

namespace BlogSite.Shared.Interfaces;

public interface IUserRepository
{

	Task<BlogSiteUser> GetUserAsync(ClaimsPrincipal user);

	Task<IEnumerable<BlogSiteUser>> GetAllUsersAsync();

	Task UpdateRoleForUserAsync(BlogSiteUser user);

}