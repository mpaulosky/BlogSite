# DevContainer Quick Reference

## 🚀 Getting Started

### Open in DevContainer
```bash
# VS Code: Ctrl+Shift+P → "Reopen in Container"
# Rider: Settings → DevContainer → Open
```

### Verify Setup
```powershell
./.devcontainer/devcontainer-setup.ps1
```

## 💻 PowerShell Essentials

### Common Commands
| Operation | PowerShell | Bash Equivalent |
|-----------|-----------|-----------------|
| List files | `Get-ChildItem` or `ls` | `ls` |
| Change directory | `Set-Location` or `cd` | `cd` |
| Print text | `Write-Host` | `echo` |
| View file | `Get-Content` or `cat` | `cat` |
| Find files | `Get-ChildItem -Recurse` | `find` |
| Environment vars | `$env:VAR_NAME` | `$VAR_NAME` |

### PowerShell Aliases
PowerShell includes familiar bash aliases:
```powershell
ls        # Get-ChildItem
cd        # Set-Location
cat       # Get-Content
pwd       # Get-Location
rm        # Remove-Item
cp        # Copy-Item
mv        # Move-Item
```

## 🔨 .NET Development

### Build & Test
```powershell
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run all tests
dotnet test

# Run specific test project
dotnet test tests/BlogSite.Web.Tests.Bunit

# Watch mode
dotnet watch --project src/BlogSite.Web
```

### Run Application
```powershell
# Start with Aspire orchestration
dotnet run --project src/BlogSite.AppHost

# Access at:
# - Aspire Dashboard: http://localhost:18888
# - Web App: http://localhost:5000
```

### E2E Tests
```powershell
# Using provided script
./build-and-test.ps1

# Direct test execution
dotnet test src/BlogSite.Tests.E2E
```

## 📦 Package Management

### .NET Workloads
```powershell
# List installed workloads
dotnet workload list

# Install Aspire workload
dotnet workload install aspire

# Update workloads
dotnet workload update
```

### NuGet Packages
```powershell
# Add package to project
dotnet add package PackageName

# Add specific version
dotnet add package PackageName --version 1.0.0

# Remove package
dotnet remove package PackageName

# List packages
dotnet list package
```

## 🐳 Docker Operations

### Container Management
```powershell
# List running containers
docker ps

# List all containers
docker ps -a

# View logs
docker logs container_name

# Execute command in container
docker exec -it container_name pwsh

# Stop container
docker stop container_name

# Remove container
docker rm container_name
```

### Images
```powershell
# List images
docker images

# Build image
docker build -t image_name .

# Pull image
docker pull image_name

# Remove image
docker rmi image_name
```

### PostgreSQL (via Aspire)
```powershell
# Start database via Aspire
dotnet run --project src/BlogSite.AppHost

# Connect using docker exec
docker exec -it postgres_container psql -U username -d articlesdb
```

## 🔀 Git Operations

### Basic Commands
```powershell
# Status
git status

# Stage changes
git add .

# Commit
git commit -m "message"

# Push
git push

# Pull
git pull

# View log
git log --oneline -10
```

### Branch Management
```powershell
# List branches
git branch

# Create branch
git checkout -b feature-name

# Switch branch
git checkout branch-name

# Delete branch
git branch -d branch-name
```

### Configuration
```powershell
# View config
git config --global --list

# Set user
git config --global user.name "Your Name"
git config --global user.email "you@example.com"

# Line endings (already configured)
git config --global core.autocrlf input
```

## 🧪 Testing

### Test Projects
```powershell
# Unit tests
dotnet test tests/BlogSite.Web.Tests.Bunit

# Integration tests  
dotnet test tests/BlogSite.Data.Postgres.Tests

# E2E tests
dotnet test src/BlogSite.Tests.E2E

# With detailed output
dotnet test --logger "console;verbosity=detailed"

# Generate coverage
dotnet test /p:CollectCoverage=true
```

### Watch Tests
```powershell
dotnet watch test --project tests/BlogSite.Web.Tests.Bunit
```

## ☁️ Azure Operations

### Azure CLI
```powershell
# Login
az login

# List subscriptions
az account list

# Set subscription
az account set --subscription "subscription-id"

# Create resource group
az group create --name rg-name --location eastus

# Deploy container app
az containerapp up --name app-name --resource-group rg-name
```

### Aspire Deployment
```powershell
# Generate manifest
dotnet run --project src/BlogSite.AppHost -- --publisher manifest

# Deploy to Azure
azd up
```

## 🔍 Troubleshooting

### Check Versions
```powershell
# All tools
./.devcontainer/devcontainer-setup.ps1

# Individual tools
dotnet --version
pwsh --version
git --version
docker --version
node --version
```

### Reset State
```powershell
# Clean build artifacts
dotnet clean

# Remove bin/obj folders
Get-ChildItem -Recurse -Directory bin,obj | Remove-Item -Recurse -Force

# Reset packages
Remove-Item -Recurse -Force ~/.nuget/packages
dotnet restore
```

### Docker Issues
```powershell
# Restart Docker
# (requires restart of DevContainer)

# Clean Docker
docker system prune -a

# Reset Docker volumes
docker volume prune
```

## 🎯 MCP Servers (AI Assistants)

### Configuration
Edit `.devcontainer/mcp-servers.json` to configure MCP servers for AI tools like Claude or Cursor.

### Available Servers
- **GitHub**: `@modelcontextprotocol/server-github`
- **Filesystem**: `@modelcontextprotocol/server-filesystem`
- **PostgreSQL**: `@modelcontextprotocol/server-postgres`
- **Fetch**: `@modelcontextprotocol/server-fetch`
- **Git**: `@modelcontextprotocol/server-git`
- **Docker**: `@modelcontextprotocol/server-docker`
- **Azure**: `@azure/mcp-server-azure`

### Usage
Configure your AI assistant to use MCP servers:
```powershell
# Install globally (if needed)
npm install -g @modelcontextprotocol/server-github

# Configure in AI tool settings
# Point to: .devcontainer/mcp-servers.json
```

## 📝 Editor Tips

### VS Code
```powershell
# Open file
code filename

# Open in new window
code -n filename

# Compare files
code --diff file1 file2

# Install extension
code --install-extension extension-id
```

### Terminal Shortcuts
- **New Terminal**: `` Ctrl+Shift+` ``
- **Split Terminal**: `Ctrl+Shift+5`
- **Switch Terminal**: `Ctrl+Tab`
- **Kill Terminal**: `Ctrl+D` or `exit`

## 🔐 Secrets Management

### Environment Variables
```powershell
# Set for current session
$env:VARIABLE_NAME = "value"

# View all
Get-ChildItem env:

# View specific
$env:PATH
```

### User Secrets
```powershell
# Initialize user secrets
dotnet user-secrets init --project src/BlogSite.Web

# Set secret
dotnet user-secrets set "Key" "Value" --project src/BlogSite.Web

# List secrets
dotnet user-secrets list --project src/BlogSite.Web
```

## 📚 Resources

- [.NET 9 Documentation](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-9)
- [.NET Aspire](https://learn.microsoft.com/dotnet/aspire/)
- [PowerShell Docs](https://learn.microsoft.com/powershell/)
- [DevContainer Spec](https://containers.dev/)
- [Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor/)

## 🆘 Help

```powershell
# PowerShell help
Get-Help command-name

# .NET help
dotnet --help
dotnet command --help

# Git help
git help
git help command

# Docker help
docker --help
docker command --help
```

---

**Tip**: PowerShell tab completion works for commands, parameters, and file paths. Press `Tab` to auto-complete!
