// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Post.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Abstractions;

/// <summary>
///   A blog post.
/// </summary>
public class Post
{

	/// <summary>
	///   The unique URL-friendly identifier for the blog post.
	/// </summary>
	[ Key]
	[ Required]
	[ MaxLength(300)]
	public required string Slug { get; set; } = string.Empty;

	/// <summary>
	///   The title of the blog post.
	/// </summary>
	[ Required]
	[ MaxLength(200)]
	public required string Title { get; set; } = string.Empty;

	/// <summary>
	///   A brief introduction or summary of the blog post.
	/// </summary>
	[ Required]
	[ MaxLength(500)]
	public required string Introduction { get; set; } = string.Empty;

	/// <summary>
	///   The main content of the article, typically in HTML format.
	/// </summary>
	[Required]
	public required string Content { get; set; } = string.Empty;

	/// <summary>
	///   The date the article was published.
	/// </summary>
	/// <value></value>
	public DateTimeOffset? PublishedOn { get; set; }

	/// <summary>
	///   The date the article was last modified.
	/// </summary>
	public DateTimeOffset? ModifiedOn { get; set; }

	/// <summary>
	///   Indicates whether the article is archived.
	/// </summary>
	[Display(Name = "Is Archived")]
	public bool IsArchived { get; set; }


	/// <summary>
	///   Converts a title into a URL-friendly slug by converting to lowercase, replacing spaces with hyphens, and URL
	///   encoding.
	/// </summary>
	/// <param name="title">The title to convert into a slug.</param>
	/// <returns>A URL-friendly slug string.</returns>
	public static string GetSlug(string title)
	{
		string slug = title.ToLower().Replace(" ", "-");

		// urlencode the slug
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