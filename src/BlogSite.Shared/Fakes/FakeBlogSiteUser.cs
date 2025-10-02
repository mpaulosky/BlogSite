// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     FakeBlogSiteUser.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlazorBlogApplication
// Project Name :  Shared
// =======================================================

namespace BlogSite.Shared.Fakes;

/// <summary>
///   Provides fake data generation methods for the <see cref="BlogSiteUser" /> entity.
/// </summary>
public static class FakeBlogSiteUser
{

	private const int Seed = 621;

	/// <summary>
	///   Generates a new fake <see cref="BlogSiteUser" /> object.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A single fake <see cref="BlogSiteUser" /> object.</returns>
	public static BlogSiteUser GetNewBlogSiteUser(bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate();

	}

	/// <summary>
	///   Generates a list of fake <see cref="BlogSiteUser" /> objects.
	/// </summary>
	/// <param name="numberRequested">The number of <see cref="BlogSiteUser" /> objects to generate.</param>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A list of fake <see cref="BlogSiteUser" /> objects.</returns>
	public static List<BlogSiteUser> GetBlogSiteUsers(int numberRequested, bool useSeed = false)
	{

		return GenerateFake(useSeed).Generate(numberRequested);

	}

	/// <summary>
	///   Generates a configured <see cref="Faker" /> for creating fake <see cref="BlogSiteUser" /> objects.
	/// </summary>
	/// <param name="useSeed">Indicates whether to apply a fixed seed for deterministic results.</param>
	/// <returns>A configured <see cref="Faker{BlogSiteUser}" /> instance.</returns>
	internal static Faker<BlogSiteUser> GenerateFake(bool useSeed = false)
	{

		Faker<BlogSiteUser>? fake = new Faker<BlogSiteUser>()
				.RuleFor(x => x.Id, Guid.CreateVersion7().ToString())
				.RuleFor(x => x.UserName, f => f.Name.FullName())
				.RuleFor(x => x.Email, (f, u) => f.Internet.Email(u.UserName))
				.RuleFor(x => x.DisplayName, f => f.Name.FirstName());

		return useSeed ? fake.UseSeed(Seed) : fake;

	}

}