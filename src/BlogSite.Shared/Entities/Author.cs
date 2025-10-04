// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Author.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Entities;

public class Author
{

	
	/// <summary>
	///   Initializes a new instance of the <see cref="Author" /> class.
	/// </summary>
	/// <param name="id">The unique identifier for the user.</param>
	/// <param name="displayName"></param>
	/// <param name="userName">The user's login name.</param>
	/// <param name="email">The user's email address.</param>
	public Author(string id, string displayName, string? userName, string? email)
	{
		Id = id;
		DisplayName = displayName;
		UserName = userName;
		Email = email;
	}

	/// <summary>
	///   Gets the unique identifier for the user.
	/// </summary>
	public string Id { get; }

	/// <summary>
	///   Gets or sets the display name of the user.
	/// </summary>
	public string DisplayName { get; set; }

	/// <summary>
	///   Gets the user's login name.
	/// </summary>
	public string? UserName { get; }

	/// <summary>
	///   Gets the user's email address.
	/// </summary>
	public string? Email { get; }

	/// <summary>
	///   Gets or sets the user's role in the system.
	/// </summary>
	public string? Role { get; set; }

}