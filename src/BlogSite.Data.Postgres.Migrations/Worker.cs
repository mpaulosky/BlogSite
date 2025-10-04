// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Worker.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Data.Postgres.Migrations
// =======================================================

using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlogSite.Data.Postgres.Migrations;

public class Worker
(
		IServiceProvider serviceProvider,
		IHostApplicationLifetime hostApplicationLifetime
) : BackgroundService
{

	public const string ActivitySourceName = "Migrations";

	private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		using (Activity? activity = s_activitySource.StartActivity("Migrating website database", ActivityKind.Client))
		{

			try
			{
				using IServiceScope scope = serviceProvider.CreateScope();
				PgContext dbContext = scope.ServiceProvider.GetRequiredService<PgContext>();

				await EnsureDatabaseAsync(dbContext, cancellationToken);
				await RunMigrationAsync(dbContext, cancellationToken);

			}
			catch (Exception ex)
			{
				activity?.AddException(ex);

				throw;
			}

		}

		hostApplicationLifetime.StopApplication();

	}

	private static async Task EnsureDatabaseAsync(DbContext dbContext, CancellationToken cancellationToken)
	{
		IRelationalDatabaseCreator dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

		if (!await dbCreator.ExistsAsync(cancellationToken))
		{
			await dbCreator.CreateAsync(cancellationToken);
		}

	}

	private static async Task RunMigrationAsync(DbContext dbContext, CancellationToken cancellationToken)
	{

		//Run migration in a transaction to avoid partial migration if it fails.
		//Await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
		await dbContext.Database.MigrateAsync(cancellationToken);

		//await transaction.CommitAsync(cancellationToken);

	}

}