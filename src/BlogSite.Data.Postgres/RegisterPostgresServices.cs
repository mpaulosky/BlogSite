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
		host.AddNpgsqlDbContext<PgContext>(Services.DATABASE, configure =>
		{
			configure.DisableRetry = disableRetry;
		});

		return host;

	}
}