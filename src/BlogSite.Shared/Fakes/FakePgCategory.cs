// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakePgCategory.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Fakes;

/// <summary>
///   Provides fake data generation methods for the <see cref="Category" /> entity.
/// </summary>
public static class FakePgCategory
{

	private const int Seed = 621;

	/// <summary>
	///   Generates a new fake <see cref="Category" /> object.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A single fake <see cref="Category" /> object.</returns>
	public static Category GetNewCategory(bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate();

	}

	/// <summary>
	///   Generates a list of fake <see cref="Category" /> objects.
	/// </summary>
	/// <param name="numberRequested">The number of <see cref="Category" /> objects to generate.</param>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A list of fake <see cref="Category" /> objects.</returns>
	public static List<Category> GetCategoriesDto(int numberRequested, bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate(numberRequested);

	}

	/// <summary>
	///   Generates a configured <see cref="Faker" /> instance to create fake <see cref="Category" /> objects.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A configured <see cref="Faker{Category}" /> instance.</returns>
	internal static Faker<Category> GenerateFake(bool useSeed = false)
	{


		Faker<Category>? fake = new Faker<Category>()
				.RuleFor(x => x.Id, _ => Random.Shared.Next(1, 1000))
				.RuleFor(x => x.CategoryName, _ => GetRandomCategoryName())
				.RuleFor(x => x.IsArchived, f => f.Random.Bool())
				.RuleFor(x => x.CreatedOn, _ => DateTimeOffset.UtcNow)
				.RuleFor(x => x.ModifiedOn, _ => DateTimeOffset.UtcNow);

		return useSeed ? fake.UseSeed(Seed) : fake;

	}

}