// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     IRunAtStartup.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Interfaces;

/// <summary>
///   Interface for services that need to run at the startup of the web application.
/// </summary>
public interface IRunAtStartup
{

	Task RunAtStartup(IServiceProvider services);

}

public interface IHasEndpoints
{

	void MapEndpoints(IServiceProvider services);

}

public interface IPluginManager
{

	Task<DirectoryInfo> CreateDirectoryInPluginsFolder(string name);

	DirectoryInfo GetDirectoryInPluginsFolder(string name);

	Task<DirectoryInfo> MoveDirectoryInPluginsFolder(string oldName, string newName);

}