var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BlogSite_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health");

builder.Build().Run();