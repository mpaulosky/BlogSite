// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Program.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Data.Postgres.Migrations
// =======================================================

using BlogSite.Data.Postgres;
using BlogSite.Data.Postgres.Migrations;
using BlogSite.ServiceDefaults;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
RegisterPostgresServices pg = new ();
pg.RegisterServices(builder, true);

// Security DB services not available; skip security migrations for now.

builder.Services.AddHostedService<Worker>();


builder.Services.AddOpenTelemetry()
		.WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

IHost host = builder.Build();
host.Run();