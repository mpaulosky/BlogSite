﻿// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Services.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlazorBlogApplication
// Project Name :  Shared
// =======================================================

namespace Shared;

/// <summary>
///   Contains constants for service names and configuration values.
/// </summary>
public static class Services
{

	// Public constants used by application and tests. Some tests expect specific
	// names/values (e.g. `SERVER` == "Server", `DATABASE` == "articlesDb").
	// Keep backwards-compatible aliases where useful.
	public const string SERVER = "Server";

	// Tests expect a `DATABASE` constant with value "articlesDb".
	public const string DATABASE = "articlesDb";

	// Backwards compatible alias used by AppHost and other code.
	// Must be unique among public string constants for tests that validate uniqueness.
	public const string ARTICLE_DATABASE = "ArticleDatabase";

	public const string USER_DATABASE = "usersDb";

	public const string WEBSITE = "Web";

	public const string CACHE = "RedisCache";

	public const string SERVER_NAME = "posts-server";

	public const string DATABASE_NAME = "articlesdb";

	public const string OUTPUT_CACHE = "output-cache";

	public const string API_SERVICE = "api-service";

	public const string CATEGORY_CACHE_NAME = "CategoryData";

	public const string ARTICLE_CACHE_NAME = "ArticleData";

	public const string ADMIN_POLICY = "AdminOnly";

	public const string DEFAULT_CORS_POLICY = "DefaultPolicy";

}