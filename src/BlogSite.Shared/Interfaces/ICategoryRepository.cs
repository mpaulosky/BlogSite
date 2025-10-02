// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     ICategoryRepository.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Interfaces;

public interface ICategoryRepository
{

	Task<Category?> GetCategory(int id);

	Task<IEnumerable<Category>> GetCategories();

	Task<IEnumerable<Category>> GetCategories(Expression<Func<Category, bool>> where);

	Task<Category> AddCategory(Category post);

	Task<Category> UpdateCategory(Category post);

	Task ArchiveCategory(int id);

}