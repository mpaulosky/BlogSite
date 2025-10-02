// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Article.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlazorBlogApplication
// Project Name :  Shared
// =======================================================

using BlogSite.Shared.Models;

namespace BlogSite.Shared.Entities;

/// <summary>
///   Represents an article in the blog system.
/// </summary>
[Serializable]
public class Article
{

	[Required, Key, MaxLength(300)]
	public required string Slug { get; set; }

	[Required, MaxLength(200)]
	public required string Title { get; set; }

	[Required, MaxLength(200)]
	public required string Introduction { get; set; }

	[Required, MaxLength(10000)]
	public required string Content { get; set; }

	[Display(Name = "Cover Image URL")] 
	public string? CoverImageUrl { get; set; }

	[Display(Name = "Url Slug")] public string? UrlSlug { get; set; }

	[Display(Name = "Published")] public bool IsPublished { get; set; }

	/// <summary>
	/// Represents the date and time when the article was published.
	/// </summary>
	[Required]
	public required DateTimeOffset? PublishedOn { get; set; }

	public required DateTimeOffset? ModifiedOn { get; set; }

	/// <summary>
	/// Indicates whether the article is archived.
	/// </summary>
	[Display(Name = "Is Archived")] public bool IsArchived { get; set; }

	/// <summary>
	///   Foreign key to the author (ApplicationUser)
	/// </summary>
	public string AuthorId { get; set; }

	/// <summary>
	///   Navigation property to the author entity
	/// </summary>
	public ApplicationUser? Author { get; set; }

	/// <summary>
	///   Foreign key to the category
	/// </summary>
	public Guid CategoryId { get; set; }

	/// <summary>
	///   Navigation property to the category entity
	/// </summary>
	public Category? Category { get; set; }

	public static explicit operator Article(ArticleDto post)
	{

		return new Article
		{
				Slug = post.Slug,
				Title = post.Title,
				Introduction = post.Introduction,
				Content = post.Content,
				PublishedOn = post.PublishedOn,
				ModifiedOn = post.ModifiedOn,
				IsPublished = post.IsPublished,
				IsArchived = post.IsArchived,
				AuthorId = post.AuthorId,
				CategoryId = post.CategoryId,
				CoverImageUrl = post.CoverImageUrl,
				UrlSlug = post.UrlSlug,
		};

	}

	public static explicit operator ArticleDto(Article post)
	{

		return new ArticleDto
		{
				Slug = post.Slug,
				Title = post.Title,
				Introduction = post.Introduction,
				Content = post.Content,
				PublishedOn = post.PublishedOn,
				ModifiedOn = post.ModifiedOn,
				IsPublished = post.IsPublished,
				IsArchived = post.IsArchived,
				AuthorId = post.AuthorId,
				CategoryId = post.CategoryId,
				CoverImageUrl = post.CoverImageUrl,
				UrlSlug = post.UrlSlug,
		};

	}

}