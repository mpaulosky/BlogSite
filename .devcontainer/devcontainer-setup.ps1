<#
.SYNOPSIS
    DevContainer setup and validation script for Windows PowerShell environment
.DESCRIPTION
    This script configures Git, validates .NET SDK installation, checks workloads,
    and ensures the development environment is ready for BlogSite development.
.NOTES
    File Name      : devcontainer-setup.ps1
    Author         : BlogSite DevContainer
    Prerequisite   : PowerShell 7+, .NET 9 SDK
#>

param(
    [switch]$SkipGitConfig
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Write-ColorOutput
{
    param(
        [string]$Message,
        [string]$Color = 'White'
    )
    Write-Host $Message -ForegroundColor $Color
}

function Test-CommandExists
{
    param([string]$Command)
    $null -ne (Get-Command $Command -ErrorAction SilentlyContinue)
}

# Banner
Write-ColorOutput "`n========================================" "Cyan"
Write-ColorOutput "  BlogSite DevContainer Setup" "Cyan"
Write-ColorOutput "  PowerShell Environment" "Cyan"
Write-ColorOutput "========================================`n" "Cyan"

# Check PowerShell version
Write-ColorOutput "Checking PowerShell version..." "Yellow"
$psVersion = $PSVersionTable.PSVersion
Write-ColorOutput "  PowerShell $($psVersion.Major).$($psVersion.Minor).$($psVersion.Patch)" "Green"

if ($psVersion.Major -lt 7)
{
    Write-ColorOutput "  Warning: PowerShell 7+ is recommended" "Yellow"
}

# Check .NET SDK
Write-ColorOutput "`nChecking .NET SDK..." "Yellow"
if (Test-CommandExists "dotnet")
{
    $dotnetVersion = dotnet --version
    Write-ColorOutput "  .NET SDK: $dotnetVersion" "Green"
    
    # Check for .NET 9
    if ($dotnetVersion -notlike "9.*")
    {
        Write-ColorOutput "  Warning: .NET 9 is recommended for this project" "Yellow"
    }
    
    # List installed workloads
    Write-ColorOutput "`nChecking .NET workloads..." "Yellow"
    $workloads = dotnet workload list 2>&1
    if ($LASTEXITCODE -eq 0)
    {
        Write-ColorOutput "  Installed workloads:" "Green"
        $workloads | Select-Object -Skip 2 | ForEach-Object {
            if ($_ -match '\S')
            {
                Write-ColorOutput "    $_" "Gray"
            }
        }
    }
    
    # Check for Aspire workload
    if ($workloads -match "aspire")
    {
        Write-ColorOutput "  ✓ Aspire workload detected" "Green"
    }
    else
    {
        Write-ColorOutput "  Note: Aspire workload not detected" "Yellow"
        Write-ColorOutput "  To install: dotnet workload install aspire" "Gray"
    }
}
else
{
    Write-ColorOutput "  Error: .NET SDK not found!" "Red"
    exit 1
}

# Check Git
Write-ColorOutput "`nChecking Git..." "Yellow"
if (Test-CommandExists "git")
{
    $gitVersion = git --version
    Write-ColorOutput "  $gitVersion" "Green"
    
    if (-not $SkipGitConfig)
    {
        Write-ColorOutput "`nConfiguring Git for cross-platform development..." "Yellow"
        
        # Set core.autocrlf to input for Linux container
        git config --global core.autocrlf input
        Write-ColorOutput "  ✓ core.autocrlf = input" "Green"
        
        # Set default branch to main
        git config --global init.defaultBranch main
        Write-ColorOutput "  ✓ init.defaultBranch = main" "Green"
        
        # Disable signing if not configured
        $signingKey = git config --global user.signingkey
        if ([string]::IsNullOrEmpty($signingKey))
        {
            git config --global commit.gpgsign false
            Write-ColorOutput "  ✓ commit.gpgsign = false (no key configured)" "Green"
        }
        
        # Show current user config
        $gitUser = git config --global user.name
        $gitEmail = git config --global user.email
        
        if (-not [string]::IsNullOrEmpty($gitUser))
        {
            Write-ColorOutput "  User: $gitUser" "Gray"
        }
        if (-not [string]::IsNullOrEmpty($gitEmail))
        {
            Write-ColorOutput "  Email: $gitEmail" "Gray"
        }
    }
}
else
{
    Write-ColorOutput "  Error: Git not found!" "Red"
    exit 1
}

# Check Docker
Write-ColorOutput "`nChecking Docker..." "Yellow"
if (Test-CommandExists "docker")
{
    $dockerVersion = docker --version
    Write-ColorOutput "  $dockerVersion" "Green"
    
    # Check if Docker daemon is running
    $dockerInfo = docker info 2>&1
    if ($LASTEXITCODE -eq 0)
    {
        Write-ColorOutput "  ✓ Docker daemon is running" "Green"
    }
    else
    {
        Write-ColorOutput "  Warning: Docker daemon may not be running" "Yellow"
    }
}
else
{
    Write-ColorOutput "  Warning: Docker not found (required for PostgreSQL)" "Yellow"
}

# Check Node.js (for MCP servers)
Write-ColorOutput "`nChecking Node.js..." "Yellow"
if (Test-CommandExists "node")
{
    $nodeVersion = node --version
    Write-ColorOutput "  Node.js: $nodeVersion" "Green"
    
    $npmVersion = npm --version
    Write-ColorOutput "  npm: $npmVersion" "Green"
}
else
{
    Write-ColorOutput "  Warning: Node.js not found (needed for MCP servers)" "Yellow"
}

# Check Azure CLI
Write-ColorOutput "`nChecking Azure CLI..." "Yellow"
if (Test-CommandExists "az")
{
    $azVersion = az version --output tsv 2>&1 | Select-Object -First 1
    Write-ColorOutput "  Azure CLI installed" "Green"
}
else
{
    Write-ColorOutput "  Azure CLI not found (optional)" "Gray"
}

# Check kubectl
Write-ColorOutput "`nChecking kubectl..." "Yellow"
if (Test-CommandExists "kubectl")
{
    $kubectlVersion = kubectl version --client --short 2>&1
    Write-ColorOutput "  kubectl installed" "Green"
}
else
{
    Write-ColorOutput "  kubectl not found (optional)" "Gray"
}

# Summary
Write-ColorOutput "`n========================================" "Cyan"
Write-ColorOutput "  Environment Check Complete" "Cyan"
Write-ColorOutput "========================================`n" "Cyan"

Write-ColorOutput "Your DevContainer is ready for development!" "Green"
Write-ColorOutput "Default terminal: PowerShell" "Cyan"
Write-ColorOutput "`nQuick Start:" "Yellow"
Write-ColorOutput "  1. Restore dependencies: dotnet restore" "Gray"
Write-ColorOutput "  2. Build solution: dotnet build" "Gray"
Write-ColorOutput "  3. Run tests: dotnet test" "Gray"
Write-ColorOutput "  4. Start application: dotnet run --project src/BlogSite.AppHost" "Gray"
Write-ColorOutput "`nFor more information, see README.md`n" "Gray"

exit 0
