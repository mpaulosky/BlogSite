// =======================================================
// Copyright (c) 2025. All rights reserved.
// File Name :     IArticleRepository.cs
// Company :       mpaulosky
// Author :        Matthew Paulosky
// Solution Name : BlogSite
// Project Name :  BlogSite.Shared
// =======================================================

namespace BlogSite.Shared.Interfaces;

public interface IArticleRepository
{

	Task<PgArticle?> GetArticle(string dateString, string slug);

	Task<IEnumerable<PgArticle>> GetArticles();

	Task<IEnumerable<PgArticle>> GetArticles(Expression<Func<PgArticle, bool>> where);

	Task<PgArticle> AddArticle(PgArticle post);

	Task<PgArticle> UpdateArticle(PgArticle post);

	Task ArchiveArticle(string slug);

}