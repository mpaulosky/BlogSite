// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     AppHost.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.AppHost
// =======================================================

using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

bool testOnly = false;

foreach (string arg in args)
{
	if (arg.StartsWith("--testonly"))
	{
		string[] parts = arg.Split('=');

		if (parts.Length == 2 && bool.TryParse(parts[1], out bool result))
		{
			testOnly = result;
		}
	}
}

(IResourceBuilder<PostgresDatabaseResource> db, IResourceBuilder<ProjectResource> migrationSvc) =
		builder.AddPostgresServices(testOnly);

builder.AddProject<BlogSite_Web>(Website)
		.WithReference(db)
		.WaitForCompletion(migrationSvc)
		.WithRunE2ETestsCommand()
		.WithExternalHttpEndpoints();

if (testOnly)
{
	// start the site with runasync and watch for a file to be created called 'stop-aspire' 
	// to stop the site
	DistributedApplication theSite = builder.Build();

	FileSystemWatcher fileSystemWatcher = new (".", "stop-aspire")
	{
			NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime
	};

	fileSystemWatcher.Created += async (_, e) =>
	{
		if (e.Name == "stop-aspire")
		{
			Console.WriteLine("Stopping the site");
			await theSite.StopAsync();
			fileSystemWatcher.Dispose();
		}
	};

	fileSystemWatcher.EnableRaisingEvents = true;

	Console.WriteLine("Starting the site in test mode");
	await theSite.RunAsync();

}
else
{
	builder.Build().Run();

}