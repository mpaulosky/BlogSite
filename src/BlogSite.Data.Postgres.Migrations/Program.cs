using BlogSite.Data.Postgres.Migrations;
using BlogSite.ServiceDefaults;
using BlogSite.Data.Postgres;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
var pg = new RegisterPostgresServices();
pg.RegisterServices(builder, disableRetry: true);

// Security DB services not available; skip security migrations for now.

builder.Services.AddHostedService<Worker>();


builder.Services.AddOpenTelemetry()
		.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

var host = builder.Build();
host.Run();