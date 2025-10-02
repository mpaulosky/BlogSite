// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Category.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlazorBlogApplication
// Project Name :  Shared
// ======================================================
namespace BlogSite.Shared.Entities;

/// <summary>
///   Represents a blog category that can be assigned to posts.
/// </summary>
public class Category
{

	/// <summary>
	/// Gets or sets the unique identifier for the category.
	/// </summary>
	[Required, Key]
	public required int Id { get; init; }

	/// <summary>
	///   The name of the category.
	/// </summary>
	[Required, MaxLength(80)]
	public required string CategoryName { get; init; }

	/// <summary>
	/// Gets the date and time when the article was created.
	/// </summary> 
	[Required, Display(Name = "Created On")]
	public required DateTimeOffset CreatedOn { get; init; }

	/// <summary>
	/// Gets or sets the date and time when the category was last modified.
	/// </summary>
	[Required]
	[Display(Name = "Modified On")]
	public DateTimeOffset? ModifiedOn { get; set; }

	/// <summary>
	/// Indicates whether the category is archived.
	/// </summary>
	[Display(Name = "Is Archived")]
	public bool IsArchived { get; set; }
}