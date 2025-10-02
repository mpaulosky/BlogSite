// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgArticle.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlazorBlogApplication
// Project Name :  Shared
// =======================================================

using BlogSite.Shared.Abstractions;

namespace BlogSite.Shared.Entities;

/// <summary>
/// Represents a blog article entity in the PostgresSQL database.
/// Implements the core functionality of an article with additional database-specific properties.
/// </summary>
[Serializable]
public class PgArticle
{

	/// <summary>
	/// Gets or sets the unique URL-friendly identifier for the article.
	/// </summary>
	[Required, Key, MaxLength(300)]
	public required string Slug { get; set; }

	/// <summary>
	/// Gets or sets the title of the article.
	/// </summary>
	[Required, MaxLength(200)]
	public required string Title { get; set; }

	/// <summary>
	/// Gets or sets the introduction or summary of the article.
	/// </summary>
	[Required, MaxLength(200)]
	public required string Introduction { get; set; }

	/// <summary>
	/// Gets or sets the main content of the article.
	/// </summary>
	[Required, MaxLength(10000)]
	public required string Content { get; set; }

	/// <summary>
	/// Gets or sets the URL of the article's cover image.
	/// </summary>
	[Display(Name = "Cover Image URL"), MaxLength(300)] 
	public string? CoverImageUrl { get; set; }

	/// <summary>
	/// Gets the date and time when the article was created.
	/// </summary> 
	[Required, Display(Name = "Created On")]
	public required DateTimeOffset CreatedOn { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether the article is published.
	/// </summary>
	[Display(Name = "Published")] public bool IsPublished { get; set; }

	/// <summary>
	/// Represents the date and time when the article was published.
	/// </summary>
	public DateTimeOffset? PublishedOn { get; set; }

	public DateTimeOffset? ModifiedOn { get; set; }

	/// <summary>
	/// Indicates whether the article is archived.
	/// </summary>
	[Display(Name = "Is Archived")] public bool IsArchived { get; set; }

	/// <summary>
	///   Foreign key to the author (ApplicationUser)
	/// </summary>
	[Required, MaxLength(50)]
	public required string AuthorId { get; set; }

	/// <summary>
	///   Navigation property to the author entity
	/// </summary>
	public BlogSiteUser? Author { get; set; }

	/// <summary>
	///   Foreign key to the category
	/// </summary>
	public int CategoryId { get; set; }

	/// <summary>
	///   Navigation property to the category entity
	/// </summary>
	public Category? Category { get; set; }

	/// <summary>
	/// Explicitly converts an Article to a PgArticle.
	/// </summary>
	/// <param name="post">The Article to convert.</param>
	/// <returns>A new PgArticle instance with properties copied from the Article.</returns>
	public static explicit operator PgArticle(Article post)
	{
		return new PgArticle
		{
				Slug = post.Slug,
				Title = post.Title,
				Introduction = post.Introduction,
				Content = post.Content,
				CreatedOn = post.CreatedOn,
				PublishedOn = post.PublishedOn,
				ModifiedOn = post.ModifiedOn,
				IsPublished = post.IsPublished,
				IsArchived = post.IsArchived,
				AuthorId = post.AuthorId,
				CategoryId = post.CategoryId,
				CoverImageUrl = post.CoverImageUrl,
		};

	}

	/// <summary>
	/// Explicitly converts a PgArticle to an Article.
	/// </summary>
	/// <param name="post">The PgArticle to convert.</param>
	/// <returns>A new ArticleDto instance with properties copied from the PgArticle.</returns>
	public static explicit operator Article(PgArticle post)
	{
		return new Article
		{
				Slug = post.Slug,
				Title = post.Title,
				Introduction = post.Introduction,
				Content = post.Content,
				CreatedOn = post.CreatedOn,
				PublishedOn = post.PublishedOn,
				ModifiedOn = post.ModifiedOn,
				IsPublished = post.IsPublished,
				IsArchived = post.IsArchived,
				AuthorId = post.AuthorId,
				Author = post.Author,
				CategoryId = post.CategoryId,
				Category = post.Category,
				CoverImageUrl = post.CoverImageUrl,
		};

	}

}