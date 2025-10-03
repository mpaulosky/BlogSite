# BlogSite DevContainer Configuration

This directory contains the DevContainer configuration for the BlogSite project, optimized for Windows-style PowerShell development workflows.

## Overview

The DevContainer provides a consistent, reproducible development environment with all necessary tools and SDKs pre-installed.

### Key Features

- **Default Terminal**: PowerShell 7+ (cross-platform)
- **.NET 9 SDK**: Latest .NET SDK with Aspire workload
- **Git**: Pre-configured for cross-platform development
- **Docker**: Docker-in-Docker for running PostgreSQL and tests
- **Node.js**: LTS version for tooling and MCP servers
- **Azure CLI**: For cloud deployments
- **kubectl**: For Kubernetes management

## Files

- **devcontainer.json**: Main DevContainer configuration
- **mcp-servers.json**: Model Context Protocol (MCP) server definitions for AI assistants
- **devcontainer-setup.ps1**: PowerShell setup and validation script

## Base Image

```
mcr.microsoft.com/devcontainers/dotnet:9.0
```

This is a Linux-based container with PowerShell cross-platform support, providing:
- Better compatibility with DevContainer features
- Full Docker support
- Native Git and development tools
- PowerShell as the default terminal

## Features Installed

| Feature | Version | Purpose |
|---------|---------|---------|
| Git | latest | Version control with Git LFS |
| GitHub CLI | latest | GitHub operations from terminal |
| Docker-in-Docker | latest | Container support for PostgreSQL |
| .NET SDK | 9.0 | .NET development and runtime |
| .NET Aspire | latest | Cloud-native orchestration |
| PowerShell | latest | Default terminal and scripting |
| Node.js | LTS | Tooling and MCP servers |
| Azure CLI | latest | Azure resource management |
| kubectl/Helm | latest | Kubernetes operations |

## VS Code Extensions

The following extensions are automatically installed:

### .NET Development
- C# Dev Kit
- C# IntelliCode
- .NET Runtime
- .NET Test Explorer

### DevOps & Cloud
- Docker
- Kubernetes Tools
- Azure Container Apps
- Azure CLI Tools

### Productivity
- GitHub Copilot & Copilot Chat
- GitLens
- PowerShell
- REST Client
- EditorConfig
- Code Spell Checker

### Documentation
- Markdown Linting
- YAML Support

## Port Forwarding

The following ports are automatically forwarded:

| Port | Service | Auto-Forward |
|------|---------|--------------|
| 5000 | Web HTTP | Notify |
| 5001 | Web HTTPS | Notify |
| 5293 | Test Site | Notify |
| 8080 | AppHost HTTP | Silent |
| 8081 | AppHost HTTPS | Silent |
| 18888 | Aspire Dashboard | Open Browser |

## MCP Servers

Model Context Protocol (MCP) servers provide AI assistants with enhanced capabilities:

### Available Servers

1. **GitHub** - Repository, issues, and PR management
2. **Filesystem** - Workspace file operations
3. **PostgreSQL** - Database queries and schema inspection
4. **Fetch** - HTTP/HTTPS API requests
5. **Git** - Version control operations
6. **Docker** - Container management
7. **Azure** - Azure resource management (optional)

### Configuration

MCP servers are defined in `mcp-servers.json`. To use them:

1. Install an MCP-compatible AI tool (Claude Desktop, Cursor, etc.)
2. Configure the tool to use `.devcontainer/mcp-servers.json`
3. Set required environment variables:
   - `GITHUB_TOKEN` for GitHub access
   - `AZURE_SUBSCRIPTION_ID` for Azure (optional)

### Usage Example

```powershell
# MCP servers enable AI assistants to:
# - Query your GitHub repositories
# - Read and write files in /workspace
# - Execute SQL queries against PostgreSQL
# - Make HTTP requests to APIs
# - Perform Git operations
# - Manage Docker containers
```

## Setup Commands

### Post-Create Command

Runs automatically when the container is first created:

```powershell
# Displays SDK versions and workloads
# Configures Git for cross-platform development
```

### Post-Start Command

Runs each time the container starts:

```powershell
Write-Host 'DevContainer Started - PowerShell is your default terminal'
```

### Manual Setup

Run the setup script manually if needed:

```powershell
./.devcontainer/devcontainer-setup.ps1
```

## Git Configuration

The container automatically configures Git for cross-platform development:

```powershell
git config --global core.autocrlf input      # Handle line endings
git config --global init.defaultBranch main  # Default branch name
```

## Environment Variables

The following environment variables are set:

- `IN_DEVCONTAINER=true` - Detects DevContainer runtime
- `RIDER_DEVCONTAINER=true` - JetBrider Rider compatibility
- `DOTNET_CLI_TELEMETRY_OPTOUT=1` - Disable telemetry
- `DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1` - Skip welcome messages

## Volume Mounts

### Persistent Data

- `.aspire` directory - Aspire configuration and data
- `E:\github` - Windows host GitHub directory (customizable)

Both mounts use `consistency=cached` for optimal performance.

## Using the DevContainer

### In VS Code

1. Install the "Remote - Containers" extension
2. Open the BlogSite folder
3. Click "Reopen in Container" when prompted
4. Wait for container build and setup

### In JetBrains Rider

1. Configure DevContainer support in Settings
2. Open the BlogSite solution
3. Select "Open in DevContainer"

### Command Line

```powershell
# Open integrated terminal (PowerShell by default)
# Run common commands:
dotnet restore
dotnet build
dotnet test
dotnet run --project src/BlogSite.AppHost
```

## Development Workflow

### 1. Restore Dependencies

```powershell
dotnet restore
```

### 2. Build Solution

```powershell
dotnet build
```

### 3. Run Tests

```powershell
# All tests
dotnet test

# Specific test project
dotnet test tests/BlogSite.Web.Tests.Bunit
```

### 4. Start Application

```powershell
# Start with Aspire orchestration
dotnet run --project src/BlogSite.AppHost

# Access Aspire Dashboard at http://localhost:18888
```

### 5. Run E2E Tests

```powershell
# Using the provided script
./build-and-test.ps1

# Or directly
dotnet test tests/BlogSite.Tests.E2E
```

## Troubleshooting

### PowerShell Not Default

If bash is still the default terminal:
1. Open VS Code settings (Ctrl+,)
2. Search for "terminal.integrated.defaultProfile.linux"
3. Ensure it's set to "pwsh"

### Docker Issues

If Docker daemon isn't running:
```powershell
# Check Docker status
docker info

# May need to restart Docker-in-Docker
```

### .NET SDK Issues

```powershell
# Verify SDK
dotnet --info

# List workloads
dotnet workload list

# Install Aspire if missing
dotnet workload install aspire
```

### Git Configuration

```powershell
# Check Git config
git config --global --list

# Reconfigure if needed
./.devcontainer/devcontainer-setup.ps1
```

## Customization

### Changing the Windows Mount

Edit `devcontainer.json` mounts section:

```json
"mounts": [
  "source=${localWorkspaceFolder}/.aspire,target=/workspace/.aspire,type=bind,consistency=cached",
  "source=C:\\Users\\YourUser\\Source,target=/workspace/source,type=bind,consistency=cached"
]
```

### Adding Extensions

Add to `devcontainer.json` extensions array:

```json
"extensions": [
  "your.extension-id"
]
```

### Adding Features

Add to `devcontainer.json` features section:

```json
"features": {
  "ghcr.io/devcontainers/features/python:1": {
    "version": "3.11"
  }
}
```

## References

- [DevContainer Specification](https://containers.dev/)
- [.NET 9 Documentation](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-9)
- [.NET Aspire Documentation](https://learn.microsoft.com/dotnet/aspire/)
- [PowerShell Documentation](https://learn.microsoft.com/powershell/)
- [Model Context Protocol](https://modelcontextprotocol.io/)
- [BlogSite Repository](https://github.com/mpaulosky/BlogSite)

## Support

For issues or questions:
1. Check this README
2. Review the main [project README](../README.md)
3. Open an issue on GitHub
