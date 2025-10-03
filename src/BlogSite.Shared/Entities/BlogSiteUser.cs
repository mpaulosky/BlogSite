namespace BlogSite.Shared.Entities;

/// <summary>
/// Represents a user in the blog site system with basic user information and role.
/// </summary>
public class BlogSiteUser
{

	/// <summary>
	/// Initializes a new instance of the <see cref="BlogSiteUser"/> class.
	/// </summary>
	/// <param name="id">The unique identifier for the user.</param>
	/// <param name="displayName"></param>
	/// <param name="userName">The user's login name.</param>
	/// <param name="email">The user's email address.</param>
	public BlogSiteUser(string id, string displayName, string? userName, string? email)
	{
		Id = id;
		DisplayName = displayName;
		UserName = userName;
		Email = email;
	}

	/// <summary>
	/// Gets the unique identifier for the user.
	/// </summary>
	public string Id { get; }

	/// <summary>
	/// Gets or sets the display name of the user.
	/// </summary>
	public string DisplayName { get; set; }

	/// <summary>
	/// Gets the user's login name.
	/// </summary>
	public string? UserName { get; }

	/// <summary>
	/// Gets the user's email address.
	/// </summary>
	public string? Email { get; }

	/// <summary>
	/// Gets or sets the user's role in the system.
	/// </summary>
	public string? Role { get; set; }

}