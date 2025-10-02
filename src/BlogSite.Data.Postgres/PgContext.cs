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
				.HasOne(a => a.Author)
				.WithMany()
				.HasForeignKey(a => a.AuthorId)
				.OnDelete(DeleteBehavior.Restrict);

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

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
	public DateTimeOffsetConverter() : base(
			v => v.UtcDateTime,
			v => v)
	{
	}
}