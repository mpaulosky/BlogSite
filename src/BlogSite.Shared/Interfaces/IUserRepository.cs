using System.Security.Claims;

namespace BlogSite.Shared.Interfaces;

public interface IUserRepository
{

	Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user);

	Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();

	Task UpdateRoleForUserAsync(ApplicationUser user);

}