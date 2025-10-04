// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     RegisterPostgresServices.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Data.Postgres
// =======================================================

using BlogSite.Shared;
using BlogSite.Shared.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogSite.Data.Postgres;

public class RegisterPostgresServices : IRegisterServices
{

	public IHostApplicationBuilder RegisterServices(IHostApplicationBuilder host, bool disableRetry = false)
	{
		// Register concrete implementations backed by PostgresSQL
		host.Services.AddTransient<IArticleRepository, PgArticleRepository>();
		host.Services.AddTransient<ICategoryRepository, PgCategoryRepository>();

		// Use the shared Services constants for database naming
		host.AddNpgsqlDbContext<PgContext>(Services.Database, configure =>
		{
			configure.DisableRetry = disableRetry;
		});

		return host;

	}

}