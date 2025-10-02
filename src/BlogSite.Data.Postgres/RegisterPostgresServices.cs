using BlogSite.Shared.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogSite.Data.Postgres;

public class RegisterPostgresServices : IRegisterServices
{
	public IHostApplicationBuilder RegisterServices(IHostApplicationBuilder host, bool disableRetry = false)
	{

		host.Services.AddTransient<IArticleRepository, ArticleRepository>();
		host.Services.AddTransient<ICategoryRepository, CategoryRepository>();
		host.AddNpgsqlDbContext<PgContext>(Constants.DBNAME, configure =>
		{
			configure.DisableRetry = disableRetry;
		});

		return host;

	}
}