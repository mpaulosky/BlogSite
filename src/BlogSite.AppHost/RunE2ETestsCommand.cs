public static class RunE2ETestsCommand
{
	private const string Name = "run-e2e-tests";

	public static IResourceBuilder<ProjectResource> WithRunE2ETestsCommand(
		this IResourceBuilder<ProjectResource> builder)
	{
		builder.WithCommand(
			Name,
			"Run end to end tests",
			_ => RunTests(),
			OnUpdateResourceState,
			iconName: "BookGlobe",
			iconVariant: IconVariant.Filled);

		return builder;
	}

	private static async Task<ExecuteCommandResult> RunTests()
	{
		ProcessStartInfo processStartInfo = new()
		{
			FileName = "dotnet",
			Arguments = "test ../../e2e/BlogSite.E2E",
			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		Process process = new() { StartInfo = processStartInfo };
		process.Start();

		string output = await process.StandardOutput.ReadToEndAsync();
		string error = await process.StandardError.ReadToEndAsync();

		await process.WaitForExitAsync();
		Console.WriteLine("E2E Tests Output: " + output);

		if (process.ExitCode == 0)
		{
			return new ExecuteCommandResult { Success = true };
		}

		return new ExecuteCommandResult { Success = false, ErrorMessage = error };
	}

	private static ResourceCommandState OnUpdateResourceState(UpdateCommandStateContext context)
	{
		var loggerFactory = context.ServiceProvider.GetRequiredService<ILoggerFactory>();
		ILogger logger = loggerFactory.CreateLogger("AppHost");
		
		if (logger.IsEnabled(LogLevel.Information))
		{
			logger.LogInformation(
				"Updating resource state: {ResourceSnapshot}",
				context.ResourceSnapshot);
		}

		return context.ResourceSnapshot.HealthStatus is HealthStatus.Healthy
			? ResourceCommandState.Enabled
			: ResourceCommandState.Disabled;

	}

}