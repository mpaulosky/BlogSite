// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgCategory.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Entities;

/// <summary>
/// Represents a data model for a blog post category in the PgCategory context.
/// Provides properties for storing category details such as name, creation, modification dates,
/// and archival status. Includes explicit conversion operators for seamless interaction
/// between PgCategory and Category models.
/// </summary>
public class PgCategory
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
	public DateTimeOffset? ModifiedOn { get; init; }

	/// <summary>
	/// Indicates whether the category is archived.
	/// </summary>
	[Display(Name = "Is Archived")]
	public bool IsArchived { get; init; }
	
	/// <summary>
	/// Explicitly converts a Category to a PgCategory.
	/// </summary>
	/// <param name="post">The Category to convert.</param>
	/// <returns>A new PgCategory instance with properties copied from the Category.</returns>
	public static explicit operator PgCategory(Category post)
	{
		return new PgCategory
		{
			Id = post.Id,
			CategoryName = post.CategoryName,
			CreatedOn = post.CreatedOn,
			ModifiedOn = post.ModifiedOn,
			IsArchived = post.IsArchived,
		};

	}

	/// <summary>
	/// Explicitly converts a PgCategory to a Category.
	/// </summary>
	/// <param name="post">The PgCategory to convert.</param>
	/// <returns>A new Category instance with properties copied from the PgCategory.</returns>
	public static explicit operator Category(PgCategory post)
	{
		return new Category
		{
			Id = post.Id,
			CategoryName = post.CategoryName,
			CreatedOn = post.CreatedOn,
			ModifiedOn = post.ModifiedOn,
			IsArchived = post.IsArchived,
		};

	}

}