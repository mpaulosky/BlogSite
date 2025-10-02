using System.Security.Claims;

namespace BlogSite.Shared.Interfaces;

public interface IUserRepository
{

	Task<BlogSiteUser> GetUserAsync(ClaimsPrincipal user);

	Task<IEnumerable<BlogSiteUser>> GetAllUsersAsync();

	Task UpdateRoleForUserAsync(BlogSiteUser user);

}