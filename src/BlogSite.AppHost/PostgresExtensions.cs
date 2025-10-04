// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     PostgresExtensions.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.AppHost
// =======================================================

using Projects;

public static class PostgresExtensions
{

	public static
			(IResourceBuilder<PostgresDatabaseResource> db,
			IResourceBuilder<ProjectResource> migrationSvc) AddPostgresServices(
					this IDistributedApplicationBuilder builder,
					bool testOnly = false)
	{
		IResourceBuilder<PostgresServerResource> dbServer = builder.AddPostgres(ServerName)
				.WithImageTag(Versions.Postgres);

		if (!testOnly)
		{
			dbServer = dbServer.WithLifetime(ContainerLifetime.Persistent)
					.WithDataVolume($"{ServerName}-data")
					.WithPgAdmin(config =>
					{
						config.WithImageTag(Versions.Pgadmin);
						config.WithLifetime(ContainerLifetime.Persistent);
					});
		}
		else
		{
			dbServer = dbServer
					.WithLifetime(ContainerLifetime.Session);
		}

		IResourceBuilder<PostgresDatabaseResource> outbounddb = dbServer.AddDatabase(DatabaseName);

		IResourceBuilder<ProjectResource> migrationSvc = builder
				.AddProject<BlogSite_Data_Postgres_Migrations>($"{ServerName}migrationsvc")
				.WithReference(outbounddb)
				.WaitFor(dbServer);

		return (outbounddb, migrationSvc);
	}

	/// <summary>
	///   A collection of version information used by the containers in this app
	/// </summary>
	private static class Versions
	{

		public const string Postgres = "17.2";

		public const string Pgadmin = "latest";

	}

}