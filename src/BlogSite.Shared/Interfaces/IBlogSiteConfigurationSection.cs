// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     IBlogSiteConfigurationSection.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Interfaces;

public interface IBlogSiteConfigurationSection
{

	string SectionName { get; }

	Task OnConfigurationChanged(IBlogSiteConfigurationSection? oldConfiguration, IPluginManager pluginManager);

}