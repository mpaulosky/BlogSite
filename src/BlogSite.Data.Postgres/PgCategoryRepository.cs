// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgCategoryRepository.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Data.Postgres
// =======================================================

using System.Linq.Expressions;

using BlogSite.Shared.Entities;
using BlogSite.Shared.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSite.Data.Postgres;

/// <summary>
/// Postgres-backed implementation of ICategoryRepository using PgContext.
/// </summary>
public class PgCategoryRepository : ICategoryRepository
{
	private readonly PgContext _context;

	public PgCategoryRepository(IServiceProvider serviceProvider)
	{
		_context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<PgContext>();
	}

	public async Task<Category?> GetCategory(int id)
	{
		return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
	}

	public async Task<IEnumerable<Category>> GetCategories()
	{
		var items = await _context.Categories.AsNoTracking().ToArrayAsync();
		return items.AsEnumerable();
	}

	public async Task<IEnumerable<Category>> GetCategories(Expression<Func<Category, bool>> where)
	{
		return await _context.Categories
			.AsNoTracking()
			.Where(c => where.Compile().Invoke(c))
			.ToArrayAsync();
	}

	public async Task<Category> AddCategory(Category post)
	{
		await _context.Categories.AddAsync(post);
		await _context.SaveChangesAsync();
		return post;
	}

	public async Task<Category> UpdateCategory(Category post)
	{
		_context.Categories.Update(post);
		await _context.SaveChangesAsync();
		return post;
	}

	public async Task ArchiveCategory(int id)
	{
		var item = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
		if (item is null) return;
		item.IsArchived = true;
		item.ModifiedOn = DateTimeOffset.UtcNow;
		_context.Categories.Update(item);
		await _context.SaveChangesAsync();
	}
}