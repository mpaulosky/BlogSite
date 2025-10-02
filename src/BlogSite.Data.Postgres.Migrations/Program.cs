using BlogSite.Data.Postgres.Migrations;

using BlogSite.Data.Postgres;
using BlogSite.Security.Postgres;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
var pg = new RegisterPostgresServices();
pg.RegisterServices(builder, disableRetry: true);

RegisterPostgresSecurityServices.ConfigurePostgresDbContext(builder, disableRetry: true);

builder.Services.AddHostedService<Worker>();


builder.Services.AddOpenTelemetry()
		.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

var host = builder.Build();
host.Run();