using System.ComponentModel.DataAnnotations;

using BlogSite.Shared.Abstractions;

namespace BlogSite.Data.Postgres;

/// <summary>
/// A postgres specific implementation of a post.
/// </summary>
public class PgPost
{

	[Required, Key, MaxLength(300)]
	public required string Slug { get; set; }

	[Required, MaxLength(200)]
	public required string Title { get; set; }

	[MaxLength(500)]
	public string? Description { get; set; }

	[Required]
	public required string Content { get; set; } = string.Empty;

	[Required]
	public required DateTimeOffset Published { get; set; } = DateTimeOffset.MaxValue;

	[Required]
	public required DateTimeOffset LastUpdate { get; set; } = DateTimeOffset.Now;

	[Required, MaxLength(11)]
	public string LanguageCode { get; set; } = "en";

	public static explicit operator PgPost(Article article)
	{

		return new PgPost
		{
			Slug = article.Slug,
			Title = article.Title,
			Description = article.Introduction,
			Content = article.Content,
			Published = article.PublishedDate,
			LastUpdate = article.LastUpdate,
			LanguageCode = article.LanguageCode,
		};

	}

	public static explicit operator Article(PgPost post)
	{

		return new Article
		{
			Slug = post.Slug,
			Title = post.Title,
			Introduction = post.Description,
			Content = post.Content,
			PublishedDate = post.Published,
			LastUpdate = post.LastUpdate,
			LanguageCode = post.LanguageCode,
		};

	}

}