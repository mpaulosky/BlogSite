// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakePgArticle.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Fakes;

/// <summary>
///   Provides fake data generation methods for the <see cref="PgArticle" /> entity.
/// </summary>
public static class FakePgArticle
{

	private const int Seed = 621;

	/// <summary>
	///   Generates a new fake <see cref="PgArticle" /> object.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A single fake <see cref="PgArticle" /> object.</returns>
	public static PgArticle GetNewPgArticle(bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate();

	}

	/// <summary>
	///   Generates a list of fake <see cref="PgArticle" /> objects.
	/// </summary>
	/// <param name="numberRequested">The number of <see cref="PgArticle" /> objects to generate.</param>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A list of fake <see cref="PgArticle" /> objects.</returns>
	public static List<PgArticle> GetPgArticles(int numberRequested, bool useSeed = false)
	{
		List<PgArticle> articles = new ();

		// Reuse a single Faker instance within this call to ensure unique items in the list.
		// For seeded runs, create a fresh seeded instance per call so repeated calls yield the same sequence.
		Faker<PgArticle>? faker = GenerateFake(useSeed);

		// Ensure CreatedOn/ModifiedOn are deterministic for seeded list generation across separate calls
		if (useSeed)
		{
			faker = faker
					.RuleFor(f => f.CreatedOn, _ => GetStaticDate())
					.RuleFor(f => f.ModifiedOn, _ => null);
		}

		for (int i = 0; i < numberRequested; i++)
		{
			PgArticle? article = faker.Generate();
			articles.Add(article);
		}

		return articles;

	}

	/// <summary>
	///   Generates a Faker instance configured to generate fake <see cref="PgArticle" /> objects.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>Configured Faker <see cref="PgArticle" /> instance.</returns>
	internal static Faker<PgArticle> GenerateFake(bool useSeed = false)
	{
		Faker<PgArticle>? fake = new Faker<PgArticle>()
				.RuleFor(a => a.Title, (f, _) => f.WaffleTitle())
				.RuleFor(a => a.Slug, (_, a) => Article.GetSlug(a.Title))
				.RuleFor(a => a.Introduction, (f, _) => f.Lorem.Sentence())
				.RuleFor(a => a.Content, (f, _) => f.WaffleMarkdown(5))
				.RuleFor(a => a.CoverImageUrl, (f, _) => f.Image.PicsumUrl())
				.RuleFor(a => a.IsPublished, (f, _) => f.Random.Bool())
				.RuleFor(a => a.PublishedOn, (_, a) => a.IsPublished ? DateTimeOffset.UtcNow : null)
				.RuleFor(a => a.IsArchived, (f, _) => f.Random.Bool())
				.RuleFor(a => a.Category, FakeCategory.GetNewCategory(useSeed))
				.RuleFor(a => a.CategoryId, (_, a) => a.CategoryId = a.Category!.Id)
				.RuleFor(a => a.Author, FakeAuthor.GetNewAuthor(useSeed))
				.RuleFor(a => a.AuthorId, (_, a) => a.AuthorId = a.Author!.Id)
				.RuleFor(a => a.CreatedOn, DateTimeOffset.UtcNow)
				.RuleFor(a => a.ModifiedOn, DateTimeOffset.UtcNow)
				.RuleFor(a => a.IsArchived, f => f.Random.Bool());

		return useSeed ? fake.UseSeed(Seed) : fake;

	}

}