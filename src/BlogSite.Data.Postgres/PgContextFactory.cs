// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PgContextFactory.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Data.Postgres
// =======================================================

using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlogSite.Data.Postgres;

/// <summary>
///   Design-time factory so EF Core tools (e.g., Add-Migration) can create PgContext without the full app host.
///   This avoids DI resolution errors during design-time.
/// </summary>
public sealed class PgContextFactory : IDesignTimeDbContextFactory<PgContext>
{
	public PgContext CreateDbContext(string[] args)
	{
		// Prefer environment variable, then standard ConnectionStrings value, then a safe local default.
		// You can override by setting: setx BLOGSITE_DB "Host=localhost;Port=5432;Database=blogsite;Username=postgres;Password=postgres" (Windows)
		string? envConn = Environment.GetEnvironmentVariable("BLOGSITE_DB");
		string? aspnetConn = Environment.GetEnvironmentVariable("ConnectionStrings:BlogSite");
		string connectionString = envConn ?? aspnetConn ??
			"Host=localhost;Port=5432;Database=blogsite;Username=postgres;Password=postgres";

		DbContextOptionsBuilder<PgContext> optionsBuilder = new();

		// Configure Npgsql provider. The extension methods are available via Npgsql.EntityFrameworkCore.PostgreSQL.
		optionsBuilder.UseNpgsql(connectionString, npgsql =>
		{
			npgsql.MigrationsHistoryTable("__EFMigrationsHistory_BlogSite");
		});

		return new PgContext(optionsBuilder.Options);
	}
}