using System.Linq.Expressions;

namespace Shared.Interfaces;

public interface IArticleRepository
{

	Task<Article?> GetArticle(string dateString, string slug);

	Task<IEnumerable<Article>> GetArticles();

	Task<IEnumerable<Article>> GetArticles(Expression<Func<Article, bool>> where);

	Task<Article> AddArticle(Article post);

	Task<Article> UpdateArticle(Article post);

	Task ArchiveArticle(string slug);

}