using System;
using System.IO;

namespace BlogSite.Shared.Helpers
{
    /// <summary>
    /// Utility helpers to detect runtime environment, including container and Dev Container detection.
    /// </summary>
    public static class RuntimeEnvironment
    {
        /// <summary>
        /// Returns true if the app is running inside any container (Docker/Podman), based on common indicators.
        /// </summary>
        public static bool IsRunningInContainer()
        {
            // .NET official images set this automatically
            if (string.Equals(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), "true", StringComparison.OrdinalIgnoreCase))
                return true;

            // Fallback: standard Docker file present in containers
            try
            {
                if (File.Exists("/.dockerenv"))
                    return true;
            }
            catch
            {
                // ignore IO permission issues
            }

            return false;
        }

        /// <summary>
        /// Returns true if running in a Dev Container (e.g., VS Code or Rider devcontainer).
        /// This repository's devcontainer.json sets RIDER_DEVCONTAINER and IN_DEVCONTAINER.
        /// </summary>
        public static bool IsRunningInDevContainer()
        {
            // Prefer explicit env vars set by .devcontainer/devcontainer.json
            var riderDev = Environment.GetEnvironmentVariable("RIDER_DEVCONTAINER");
            if (string.Equals(riderDev, "true", StringComparison.OrdinalIgnoreCase))
                return true;

            var inDev = Environment.GetEnvironmentVariable("IN_DEVCONTAINER");
            if (string.Equals(inDev, "true", StringComparison.OrdinalIgnoreCase))
                return true;

            // Heuristics: some platforms set these
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CODESPACES")))
                return true;
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITPOD_WORKSPACE_ID")))
                return true;

            return false;
        }
    }
}