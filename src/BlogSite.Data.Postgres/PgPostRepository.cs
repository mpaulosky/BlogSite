// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgArticleRepository.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

using System.Globalization;

using BlogSite.Shared.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace BlogSite.Shared.Repositories;

public class PgArticleRepository : IArticleRepository
{

	private readonly PgContext Context;

	public PgArticleRepository(IServiceProvider serviceProvider)
	{
		Context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PgContext>();
	}

	public async Task<Article> AddArticle(Article post)
	{
		// add a post to the database
		post.PublishedDate = DateTimeOffset.Now;
		post.LastUpdate = DateTimeOffset.Now;
		await Context.Articles.AddAsync((PgArticle)post);
		await Context.SaveChangesAsync();

		return post;
	}

	public async Task DeleteArticle(string slug)
	{
		// delete a post from the database based on the slug submitted
		var post = await Context.Articles.FirstOrDefaultAsync(p => p.Slug == slug);

		if (post != null)
		{
			Context.Articles.Remove(post);
			await Context.SaveChangesAsync();
		}
	}

	public async Task<Article?> GetArticle(string dateString, string slug)
	{

		if (string.IsNullOrEmpty(dateString) || string.IsNullOrEmpty(slug))
		{
			return null;
		}

		DateTimeOffset theDate = DateTimeOffset.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture,
				DateTimeStyles.AssumeUniversal);

		// get a post from the database based on the slug submitted
		var theArticles = await Context.Articles
				.AsNoTracking()
				.Where(p => p.Slug == slug)
				.Select(p => (Article)p)
				.ToArrayAsync();

		return theArticles.FirstOrDefault(p =>
				p.PublishedDate.UtcDateTime.Date == theDate.UtcDateTime.Date);

	}

	public async Task<IEnumerable<Article>> GetArticles()
	{
		// get all posts from the database
		var posts = await Context.Articles.AsNoTracking().ToArrayAsync();

		return posts.Select(p => (Article)p);
	}

	public async Task<IEnumerable<Article>> GetArticles(Expression<Func<Article, bool>> where)
	{
		// get all posts from the database based on the where clause
		return await Context.Articles
				.AsNoTracking()
				.Where(p => where.Compile().Invoke((Article)p))
				.Select(p => (Article)p)
				.ToArrayAsync();

	}

	public async Task<Article> UpdateArticle(Article post)
	{
		// update a post in the database
		post.LastUpdate = DateTimeOffset.Now;
		Context.Articles.Update((PgArticle)post);
		await Context.SaveChangesAsync();

		return post;

	}

}