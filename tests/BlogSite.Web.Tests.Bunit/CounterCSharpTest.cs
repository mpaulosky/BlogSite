// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     CounterCSharpTest.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Web.Tests.Bunit
// =======================================================

namespace BlogSite.Web;

/// <summary>
///   These tests are written entirely in C#.
///   Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
/// </summary>

// The full qualified namespace for bUnit TestContext is used here as xunit v3 also offers a TestContext class
public class CounterCSharpTest : BunitContext
{

	[Fact]
	public void CounterStartsAtZero()
	{
		// Arrange
		IRenderedComponent<Counter> cut = Render<Counter>();

		// Assert that the content of the paragraph shows a counter at zero
		cut.Find("p").MarkupMatches("<p>Current count: 0</p>");
	}

	[Fact]
	public void ClickingButtonIncrementsCounter()
	{
		// Arrange
		IRenderedComponent<Counter> cut = Render<Counter>();

		// Act - click button to increment counter
		cut.Find("button").Click();

		// Assert that the counter was incremented
		cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
	}

}