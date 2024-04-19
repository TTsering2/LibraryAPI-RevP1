using Libraries.Models;

namespace Libraries.Services;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author> GetAnAuthorAsync(int authorId);
    Task UpdateAnAuthorAsync(int authorId, Author updatedAuthor);
    Task DeleteAnAuthorAsync(int authorId);
    Task<Author> CreateAnAuthorAsync(Author author);
}