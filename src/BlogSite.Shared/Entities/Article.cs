// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Article.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Entities;

/// <summary>
///   A blog post.
/// </summary>
public class Article
{

	/// <summary>
	///   The unique URL-friendly identifier for the blog post.
	/// </summary>
	[Required, Key, MaxLength(300)] public required string Slug { get; init; } = string.Empty;

	/// <summary>
	///   The title of the blog post.
	/// </summary>
	[Required, MaxLength(200)] public required string Title { get; init; } = string.Empty;

	/// <summary>
	///   A brief introduction or summary of the blog post.
	/// </summary>
	[Required, MaxLength(500)] public required string Introduction { get; init; } = string.Empty;

	/// <summary>
	///   The main content of the article, typically in HTML format.
	/// </summary>
	[Required] public required string Content { get; init; } = string.Empty;
	
	/// <summary>
	/// Gets or sets the URL of the article's cover image.
	/// </summary>
	[Display(Name = "Cover Image URL"), MaxLength(300)] public string? CoverImageUrl { get; init; }

	/// <summary>
	/// Gets the date and time when the article was created.
	/// </summary> 
	[Required, Display(Name = "Created On")]
	public required DateTimeOffset CreatedOn { get; init; } = DateTime.UtcNow;

	/// <summary>
	/// Gets or sets a value indicating whether the article is published.
	/// </summary>
	[Display(Name = "Published")] public bool IsPublished { get; init; }

	/// <summary>
	///   The date the article was published.
	/// </summary>
	/// <value></value>
	public DateTimeOffset? PublishedOn { get; init; }

	/// <summary>
	///   The date the article was last modified.
	/// </summary>
	public DateTimeOffset? ModifiedOn { get; init; }

	/// <summary>
	///   Indicates whether the article is archived.
	/// </summary>
	[Display(Name = "Is Archived")]
	public bool IsArchived { get; init; }

	/// <summary>
	///   Foreign key to the author (ApplicationUser)
	/// </summary>
	public required string AuthorId { get; set; }

	/// <summary>
	///   Gets or sets the author information of the article.
	/// </summary>
	public BlogSiteUser? Author { get; set; }

	/// <summary>
	///   Foreign key to the category
	/// </summary>
	public int CategoryId { get; set; }

	/// <summary>
	///   Gets or sets the category information of the article.
	/// </summary>
	public Category? Category { get; set; }

	/// <summary>
	///   Converts a title into a URL-friendly slug by converting to lowercase, replacing spaces with hyphens, and URL
	///   encoding.
	/// </summary>
	/// <param name="title">The title to convert into a slug.</param>
	/// <returns>A URL-friendly slug string.</returns>
	public static string GetSlug(string title)
	{
		string slug = title.ToLower().Replace(" ", "-");

		// url encode the slug
		slug = HttpUtility.UrlEncode(slug);

		return slug;
	}

	/// <summary>
	///   Generates a relative URI for the blog post in the format "/YYYYMMDD/slug".
	/// </summary>
	/// <returns>A relative URI pointing to the blog post.</returns>
	public Uri ToUrl()
	{
		return new Uri($"/{PublishedOn?.UtcDateTime.ToString("yyyyMMdd") ?? DateTime.UtcNow.ToString("yyyyMMdd")}/{Slug}",
				UriKind.Relative);
	}

}