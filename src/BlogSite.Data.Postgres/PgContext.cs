// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgContext.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Data.Postgres
// =======================================================

using BlogSite.Shared.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogSite.Data.Postgres;

public class PgContext : DbContext
{

	public PgContext(DbContextOptions<PgContext> options) : base(options) { }

	public DbSet<Category> Categories => Set<Category>();

	public DbSet<PgArticle> Articles => Set<PgArticle>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		modelBuilder
				.Entity<PgArticle>()
				.HasIndex(p => p.Slug)
				.IsUnique();

		modelBuilder
				.Entity<PgArticle>()
				.Ignore(a => a.Author);

		modelBuilder.Entity<PgArticle>()
				.HasOne(a => a.Category)
				.WithMany()
				.HasForeignKey(a => a.CategoryId)
				.OnDelete(DeleteBehavior.Restrict);

		modelBuilder
				.Entity<PgArticle>()
				.Property(e => e.PublishedOn)
				.HasConversion(new DateTimeOffsetConverter());

		modelBuilder
				.Entity<PgArticle>()
				.Property(e => e.ModifiedOn)
				.HasConversion(new DateTimeOffsetConverter());

		modelBuilder.Entity<Category>()
				.HasIndex(p => p.Id)
				.IsUnique();

		modelBuilder
				.Entity<Category>()
				.Property(e => e.ModifiedOn)
				.HasConversion(new DateTimeOffsetConverter());

	}

}

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTime>
{
	public DateTimeOffsetConverter() : base(
		v => v.UtcDateTime,
		v => new DateTimeOffset(DateTime.SpecifyKind(v, DateTimeKind.Utc))) { }
}