// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     RegisterPluginAttribute.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Interfaces;

public class RegisterPluginAttribute : Attribute
{

	public RegisterPluginAttribute(PluginServiceLocatorScope scope, PluginRegisterType registerType)
	{
		Scope = scope;
		RegisterType = registerType;
	}

	public PluginServiceLocatorScope Scope { get; }

	public PluginRegisterType RegisterType { get; }

}

public enum PluginServiceLocatorScope
{

	Transient,

	Singleton,

	Scoped

}

public enum PluginRegisterType
{

	FileStorage

}