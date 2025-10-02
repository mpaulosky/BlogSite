// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgArticleRepository.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

using System.Globalization;
using System.Linq.Expressions;

using BlogSite.Shared.Entities;
using BlogSite.Shared.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSite.Data.Postgres;

public class PgArticleRepository : IArticleRepository
{

	private readonly PgContext _context;

	public PgArticleRepository(IServiceProvider serviceProvider)
	{
		_context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PgContext>();
	}

	public async Task<PgArticle> AddArticle(PgArticle post)
	{
		// add a post to the database
		post.PublishedOn = DateTimeOffset.Now;
		post.ModifiedOn = DateTimeOffset.Now;
		await _context.Articles.AddAsync((PgArticle)post);
		await _context.SaveChangesAsync();

		return post;
	}

	public async Task ArchiveArticle(string slug)
	{
		// Archive a post in the database based on the slug submitted
		var post = await _context.Articles.FirstOrDefaultAsync(p => p.Slug == slug);

		if (post != null)
		{

			// Set the post as archived in the database
			post.ModifiedOn = DateTimeOffset.Now;
			post.IsArchived = true;

			_context.Articles.Update((PgArticle)post);

			await _context.SaveChangesAsync();

		}
	}

	public async Task<PgArticle?> GetArticle(string dateString, string slug)
	{

		if (string.IsNullOrEmpty(dateString) || string.IsNullOrEmpty(slug))
		{
			return null;
		}

		DateTimeOffset theDate = DateTimeOffset.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture,
				DateTimeStyles.AssumeUniversal);

		// get a post from the database based on the slug submitted
		var theArticles = await _context.Articles
				.AsNoTracking()
				.Where(p => p.Slug == slug)
				.Select(p => (PgArticle)p)
				.ToArrayAsync();

		return theArticles.FirstOrDefault(p =>
				(p.PublishedOn ?? DateTimeOffset.MinValue).UtcDateTime.Date == theDate.UtcDateTime.Date);

	}

	public async Task<IEnumerable<PgArticle>> GetArticles()
	{
		// get all posts from the database
		var posts = await _context.Articles.AsNoTracking().ToArrayAsync();

		return posts.Select(p => (PgArticle)p);
	}

	public async Task<IEnumerable<PgArticle>> GetArticles(Expression<Func<PgArticle, bool>> where)
	{
		// get all posts from the database based on the where clause
		return await _context.Articles
				.AsNoTracking()
				.Where(p => where.Compile().Invoke((PgArticle)p))
				.Select(p => (PgArticle)p)
				.ToArrayAsync();

	}

	public async Task<PgArticle> UpdateArticle(PgArticle post)
	{

		// update a post in the database
		post.ModifiedOn = DateTimeOffset.Now;

		_context.Articles.Update((PgArticle)post);
		await _context.SaveChangesAsync();

		return post;

	}

}