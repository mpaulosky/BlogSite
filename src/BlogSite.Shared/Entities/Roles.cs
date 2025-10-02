// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     Roles.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlazorBlogApplication
// Project Name :  Shared
// =======================================================

namespace BlogSite.Shared.Entities;

public static class Roles
{
	public const string Admin = "Admin";
	public const string Author = "Author";
	public const string User = "User";
	public const string AdminUsers = "Admin";
	public const string AuthorUsers = "Admin,Author";
	public const string AllUsers = "Admin,Author,User";

	public static string[] AllRoles = [Admin, Author, User];

}